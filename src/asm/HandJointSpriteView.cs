using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HandJointSpriteView : MonoBehaviour
{
	[SerializeField]
	private string handJointTexturePath = "hand_points/hand_3";

	[SerializeField]
	private float pixelsPerUnit = 100f;

	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		ApplyTexturePath(handJointTexturePath);
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
		Texture2D texture2D = Resources.Load<Texture2D>(texturePath);
		if (texture2D == null)
		{
			Debug.LogError("HandJointSpriteView: cannot load texture Resources/" + texturePath);
			return;
		}
		Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
		if (_spriteRenderer == null)
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		_spriteRenderer.sprite = sprite;
	}
}
