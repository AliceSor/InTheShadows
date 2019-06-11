using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationLifeCycle : MonoBehaviour
{
	[SerializeField] GameObject closePanel;
	public LvlController lvlController;

	void Start ()
	{
		if (lvlController.applicationLifeCycle != null)
		{
			if (lvlController.applicationLifeCycle != this)
				Destroy(gameObject);
		}
		else
		{
			lvlController.applicationLifeCycle = this;
		}
		DontDestroyOnLoad(this);
	}
	
	void Update ()
	{
		if (Input.GetKey("escape"))
            closePanel.SetActive(true);
	}

	public void Exit()
	{
		closePanel.SetActive(true);
	}
}
