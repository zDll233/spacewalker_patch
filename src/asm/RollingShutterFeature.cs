using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RollingShutterFeature : ScriptableRendererFeature
{
	[Serializable]
	public class Settings
	{
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
	}

	private sealed class RollingShutterPass : ScriptableRenderPass
	{
		private readonly Material _mat;

		private RTHandle _temp;

		public RollingShutterPass(Material mat)
		{
			_mat = mat;
			base.profilingSampler = new ProfilingSampler("RollingShutter");
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (_mat == null)
			{
				return;
			}
			RTHandle cameraColorTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
			if (cameraColorTargetHandle != null && !(cameraColorTargetHandle.rt == null))
			{
				RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
				descriptor.depthBufferBits = 0;
				descriptor.msaaSamples = 1;
				RenderingUtils.ReAllocateIfNeeded(ref _temp, in descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, isShadowMap: false, 1, 0f, "_RollingShutterTemp");
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, base.profilingSampler))
				{
					Blitter.BlitCameraTexture(commandBuffer, cameraColorTargetHandle, _temp, _mat, 0);
					Blitter.BlitCameraTexture(commandBuffer, _temp, cameraColorTargetHandle);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				CommandBufferPool.Release(commandBuffer);
			}
		}

		public void Dispose()
		{
			_temp?.Release();
			_temp = null;
		}
	}

	private const string ShaderName = "Hidden/SpaceWalker/RollingShutter";

	public Settings settings = new Settings();

	[SerializeField]
	[HideInInspector]
	private Shader shader;

	private Material _material;

	private RollingShutterPass _pass;

	public override void Create()
	{
		if (shader == null)
		{
			shader = Shader.Find("Hidden/SpaceWalker/RollingShutter");
		}
		if (shader != null)
		{
			_material = CoreUtils.CreateEngineMaterial(shader);
		}
		_pass = new RollingShutterPass(_material)
		{
			renderPassEvent = settings.renderPassEvent
		};
	}

	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		if (!(_material == null) && _pass != null && RollingShutter.Active && renderingData.cameraData.cameraType == CameraType.Game)
		{
			renderer.EnqueuePass(_pass);
		}
	}

	protected override void Dispose(bool disposing)
	{
		_pass?.Dispose();
		CoreUtils.Destroy(_material);
		_material = null;
	}
}
