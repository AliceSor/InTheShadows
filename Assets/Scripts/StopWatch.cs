using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
	private Text text;
	public float time
	{
		get
		{
			return _time;
		}
	}

	private float _time = 0f;
	private bool stop = false;

	void Start()
	{
		text = GetComponent<Text>();
	}

	public void StartStopWatch()
	{
		StartCoroutine(Counting());
	}

	public float Stop()
	{
		stop = true;
		return _time;
	}

	private IEnumerator Counting()
	{
		while (!stop)
		{
			text.text = "Time : " + _time;
			yield return new WaitForSeconds(1f);
			_time++;
		}
	}

}
