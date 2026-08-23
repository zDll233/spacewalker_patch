using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
	private Camera _cam;

	[SerializeField]
	private string handJointTexturePath = "hand_points/hand_3";

	[SerializeField]
	private float pixelsPerUnit = 100f;

	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_cam = Camera.main;
		_spriteRenderer = GetComponent<SpriteRenderer>();
		if (_spriteRenderer != null)
		{
			ApplyTexturePath(handJointTexturePath);
		}
	}

	public void SetTexturePath(string texturePath)
	{
		if (!string.IsNullOrWhiteSpace(texturePath))
		{
			handJointTexturePath = texturePath;
			ApplyTexturePath(handJointTexturePath);
		}
	}

	private void ApplyTexturePath(string texturePath)
	{
		if (_spriteRenderer == null)
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			if (_spriteRenderer == null)
			{
				return;
			}
		}
		Texture2D texture2D = Resources.Load<Texture2D>(texturePath);
		if (texture2D == null)
		{
			Debug.LogError("BillboardSprite: cannot load texture Resources/" + texturePath);
			return;
		}
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
		_spriteRenderer.sprite = sprite;
	}

	private void LateUpdate()
	{
		if (_cam == null)
		{
			_cam = Camera.main;
		}
		else
		{
			base.transform.rotation = _cam.transform.rotation;
		}
	}
}
