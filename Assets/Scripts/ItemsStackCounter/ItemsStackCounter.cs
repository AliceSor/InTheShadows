using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsStackCounter : MonoBehaviour
{
	public StackItem[] items;

	/// <summary>
	/// Take float in percents and display rounded down, judging by nubmer of items
	/// </summary>
	public void Show(float percent)
	{
		int num = Mathf.FloorToInt((percent / 100) * items.Length);

		Debug.Log("Num " + num);
		for (int i = 0; i < items.Length; i++)
		{
			if (i < num)
				items[i].Show();
			else
			{
				if (items[i].IsVisible())
					items[i].Hide();
			}
		}
	}
}
