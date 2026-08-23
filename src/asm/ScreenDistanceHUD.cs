using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDistanceHUD : MonoBehaviour
{
	private MainCamera _mainCamera;

	private GameObject _canvasGo;

	private GameObject _resizeBg;

	private GameObject _recenterBg;

	private TextMeshProUGUI _distanceText;

	private Coroutine _hideCoroutine;

	private bool _handResizing;

	private bool _isRecenterMode;

	private const float AutoHideDelay = 2f;

	private volatile bool _pendingResizeStart;

	private volatile bool _pendingResizeEnd;

	private volatile bool _pendingRecenter;

	private void Awake()
	{
		_mainCamera = GetComponent<MainCamera>();
		BuildUI();
		SetVisible(visible: false);
		HandJointsVisualizer.HandPoseChanged += OnHandPoseChanged;
		HandJointsVisualizer.HandRecenterTriggered += OnHandRecenter;
	}

	private void OnDestroy()
	{
		HandJointsVisualizer.HandPoseChanged -= OnHandPoseChanged;
		HandJointsVisualizer.HandRecenterTriggered -= OnHandRecenter;
		if (_canvasGo != null)
		{
			Object.Destroy(_canvasGo);
		}
	}

	private void BuildUI()
	{
		_canvasGo = new GameObject("ScreenDistanceHUDCanvas");
		Canvas canvas = _canvasGo.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 100;
		CanvasScaler canvasScaler = _canvasGo.AddComponent<CanvasScaler>();
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
		canvasScaler.matchWidthOrHeight = 0.5f;
		_canvasGo.AddComponent<GraphicRaycaster>();
		_resizeBg = CreateBgPanel(_canvasGo.transform, "ResizeBg", "scale_bg");
		GameObject gameObject = new GameObject("DistanceText");
		gameObject.transform.SetParent(_resizeBg.transform, worldPositionStays: false);
		_distanceText = gameObject.AddComponent<TextMeshProUGUI>();
		_distanceText.alignment = TextAlignmentOptions.Center;
		_distanceText.fontSize = 42f;
		_distanceText.color = Color.white;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector2.one;
		component.offsetMin = Vector2.zero;
		component.offsetMax = Vector2.zero;
		_recenterBg = CreateBgPanel(_canvasGo.transform, "RecenterBg", "recenter_hud");
	}

	private static GameObject CreateBgPanel(Transform parent, string name, string textureName, float width = 300f, float height = 90f)
	{
		GameObject obj = new GameObject(name);
		obj.transform.SetParent(parent, worldPositionStays: false);
		Image image = obj.AddComponent<Image>();
		Texture2D texture2D = Resources.Load<Texture2D>(textureName);
		if (texture2D != null)
		{
			image.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
			float num = (float)texture2D.width / (float)texture2D.height;
			height = width / num;
		}
		image.type = Image.Type.Simple;
		image.preserveAspect = true;
		RectTransform component = obj.GetComponent<RectTransform>();
		component.anchorMin = new Vector2(0.5f, 0.2f);
		component.anchorMax = new Vector2(0.5f, 0.2f);
		component.pivot = new Vector2(0.5f, 0.5f);
		component.anchoredPosition = Vector2.zero;
		component.sizeDelta = new Vector2(width, height);
		return obj;
	}

	private void Update()
	{
		if (_pendingResizeStart)
		{
			_pendingResizeStart = false;
			_handResizing = true;
			_isRecenterMode = false;
			CancelHide();
			ApplyMode();
			SetVisible(visible: true);
		}
		if (_pendingResizeEnd)
		{
			_pendingResizeEnd = false;
			_handResizing = false;
			ShowThenHide(2f);
		}
		if (_pendingRecenter)
		{
			_pendingRecenter = false;
			_isRecenterMode = true;
			ApplyMode();
			ShowThenHide(2f);
		}
		if (_canvasGo != null && _canvasGo.activeSelf && !_isRecenterMode && _mainCamera != null)
		{
			float num = DistanceToPercent(_mainCamera.ScreenDistance);
			_distanceText.text = num.ToString("F0") + "%";
		}
	}

	private static float DistanceToPercent(float distance)
	{
		if (distance <= 1f)
		{
			return Mathf.Lerp(200f, 100f, Mathf.InverseLerp(0.5f, 1f, distance));
		}
		return Mathf.Lerp(100f, 50f, Mathf.InverseLerp(1f, 2f, distance));
	}

	private void OnHandPoseChanged(bool resizing, float ratio)
	{
		if (resizing)
		{
			_pendingResizeStart = true;
		}
		else
		{
			_pendingResizeEnd = true;
		}
	}

	private void OnHandRecenter()
	{
		_pendingRecenter = true;
	}

	public void ShowThenHide(float delay)
	{
		SetVisible(visible: true);
		CancelHide();
		_hideCoroutine = StartCoroutine(HideAfter(delay));
	}

	private void CancelHide()
	{
		if (_hideCoroutine != null)
		{
			StopCoroutine(_hideCoroutine);
			_hideCoroutine = null;
		}
	}

	private IEnumerator HideAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		if (!_handResizing)
		{
			SetVisible(visible: false);
		}
		_hideCoroutine = null;
	}

	private void SetVisible(bool visible)
	{
		if (_canvasGo != null)
		{
			_canvasGo.SetActive(visible);
		}
	}

	private void ApplyMode()
	{
		if (_resizeBg != null)
		{
			_resizeBg.SetActive(!_isRecenterMode);
		}
		if (_recenterBg != null)
		{
			_recenterBg.SetActive(_isRecenterMode);
		}
	}
}
