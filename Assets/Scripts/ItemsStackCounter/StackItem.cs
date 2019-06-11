using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackItem : MonoBehaviour, IStackItem
{
	public Vector2 rectsSizeOpened;
	public Vector2 rectsSizeClosed;
	public Sprite closedSprite;
	public Sprite openSprite;

	Image img;
	public bool isVisible = false;

	private RectTransform rect;

	private void Start()
	{
		rect = GetComponent<RectTransform>();
		img = GetComponent<Image>();
	}

	public virtual void Hide()
	{
		rect.sizeDelta = rectsSizeClosed;
		img.sprite = closedSprite;
	}

	public virtual void Show()
	{
		//rect.sizeDelta = rectsSizeOpened;
		img = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
		img.sprite = openSprite;

		rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  rectsSizeOpened.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  rectsSizeOpened.y);
	}

	public bool IsVisible()
	{
		return isVisible;
	}
}
