using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public LvlController lvlController;
	public GameObject modelsHolder;
	public float rotationSpeed;
	public LevelData data;
	public Transform light;
	public Transform anchor0;
	public Transform anchor1;
	public float smoothRotateTime;
	public bool blockWhenWin = false;

	public ItemsStackCounter tipsDisplayer;
	public GameObject winPanel;
	public StopWatch stopWatch;

	private Rigidbody rb0, rb1, rbCur, rbAll;
	private Transform t0, t1, tCur, tAll;
	private Quaternion winRotation0, winRotation1, winRotationAll, winRotation0_1, winRotation1_1, winRotationAll_1;
	private bool pause = false;
	private bool win = false;
	private bool oneModelMode = true;
	private bool firstActive = true;
	private float time;

	void Awake()
	{
		Debug.Log("Awake");
		if (lvlController.data != null)
			data = lvlController.data;
	}

	void Start()
	{
		Debug.Log("Start");
		SetupScene();

		StartCoroutine(ManageRotation());
		stopWatch.StartStopWatch();
	}

	private void SetupScene()
	{
		if (data == null)
			Destroy(gameObject);
		oneModelMode = data.mode == LevelMode.ONE_MODEL;
		if (oneModelMode)
		{
			GameObject m0 = Instantiate(data.model0, modelsHolder.transform, false);
			tCur = m0.transform;
			tCur.rotation = Quaternion.Euler(data.startRotation0);
			rbCur = m0.GetComponent<Rigidbody>();
			winRotation0 = Quaternion.Euler(data.winRotation0);
			winRotation0_1 = Quaternion.Euler(data.winRotation0_1);
		}
		else
		{
			GameObject m0 = Instantiate(data.model0, anchor0.position, Quaternion.Euler(data.startRotation0), modelsHolder.transform);
			GameObject m1 = Instantiate(data.model1, anchor1.position, Quaternion.Euler(data.startRotation0), modelsHolder.transform);

			t0 = m0.transform;
			rb0 = m0.GetComponent<Rigidbody>();
			t1 = m1.transform;
			rb1 = m1.GetComponent<Rigidbody>();
			tCur = m0.transform;
			rbCur = m0.GetComponent<Rigidbody>();
			rbAll = modelsHolder.GetComponent<Rigidbody>();
			tAll = modelsHolder.transform;

			winRotation0 = Quaternion.Euler(data.winRotation0);
			winRotation1 = Quaternion.Euler(data.winRotation1);
			winRotationAll = Quaternion.Euler(data.winRotationAll);
			winRotation0_1 = Quaternion.Euler(data.winRotation0_1);
			winRotation1_1 = Quaternion.Euler(data.winRotation1_1);
			winRotationAll_1 = Quaternion.Euler(data.winRotationAll_1);
		}
	}//SetupScene

	IEnumerator ManageRotation()
	{
		while (true)
		{
			if (!pause)
			{
				if (!win || !blockWhenWin)
					CheckInput();
				if (!win)
					CheckRotation();
				if (Input.GetKeyUp(KeyCode.P))
				{
					if (!oneModelMode)
						Debug.Log("t0 :" + t0.rotation.eulerAngles + " t1: " + t1.rotation.eulerAngles + " tAll: " + tAll.rotation.eulerAngles);
					else
					{
						Debug.Log("t0 :" + tCur.rotation.eulerAngles);
					}
				}	
			}
			yield return null;
		}
	}

	private void CheckRotation()
	{
		if (oneModelMode)
		{
			float angle = Quaternion.Angle(tCur.rotation, winRotation0);
			float angle_1 = Quaternion.Angle(tCur.rotation, winRotation0_1);

			angle = (angle < angle_1) ? angle: angle_1;

			if (angle < 90)
			{
				float percent = ((180 - angle) / (180 - data.precision0)) * 100;
				tipsDisplayer.Show(percent);
			}
			if (angle <= data.precision0)
				YouWin();
		}
		else
		{
			float c0 = Quaternion.Angle(t0.rotation, winRotation0);
			float c1 = Quaternion.Angle(t1.rotation, winRotation1);
			float c2 = Quaternion.Angle(tAll.rotation, winRotationAll);

			float c0_1 = Quaternion.Angle(t0.rotation, winRotation0_1);
			float c1_1 = Quaternion.Angle(t1.rotation, winRotation1_1);
			float c2_1 = Quaternion.Angle(tAll.rotation, winRotationAll_1);

			c0 = (c0 < c0_1) ? c0 : c0_1;
			c1 = (c1 < c1_1) ? c1 : c1_1;
			c2 = (c2 < c2_1) ? c2 : c2_1;
			
			float percent = 0;
			if (c0 <= data.precision0 + 20) percent += 20;
			if (c0 <= data.precision0) percent += 20;
			if (c1 <= data.precision1 + 20) percent += 20;
			if (c1 <= data.precision1) percent += 20;
			if (c2 <= data.precisionAll + 20) percent += 10;
			if (c2 <= data.precisionAll) percent += 10;
			Debug.Log("C0 " + c0 + " C1 " + c1 + " C2 " + c2 + " Succes: " + percent);
			tipsDisplayer.Show(percent);

			//if (c0 <= data.precision0 && c1 <= data.precision1 && c2 <= data.precisionAll)
			if (percent >= 98)
			{
				YouWin();
			}
		}
	}

	private void YouWin()
	{
		winPanel.SetActive(true);
		Debug.Log( "You win");
		
		win = true;
		lvlController.LevelDone(stopWatch.Stop());
		StartCoroutine(SmoothRotate());
	}

	private IEnumerator SmoothRotate()
	{
		bool wasBlocked = blockWhenWin;
		blockWhenWin = true;
		float rate = 1.0f / smoothRotateTime;
        float t = 0.0f;
        while(t <= 1.0f)
        {
            t += Time.deltaTime * rate;
			if (!oneModelMode)
			{
           		Quaternion temp0 = t0.rotation;
				Quaternion temp1 = t1.rotation;
           		tAll.rotation = Quaternion.Slerp(tAll.rotation, winRotationAll, t);
				t0.rotation = Quaternion.Slerp(temp0, winRotation0, t);
           		t1.rotation = Quaternion.Slerp(temp1, winRotation1, t);
			}
			else
			{
				tCur.rotation = Quaternion.Slerp(tCur.rotation, winRotation0, t);
			}
            yield return null;
        }
		
		blockWhenWin = wasBlocked;
	}//SmoothRotate

	private void CheckInput()
	{
		bool aroundZ = Input.GetKey(KeyCode.Z) && Input.GetMouseButton(0);
		bool rotateBothaAroundZ = !oneModelMode && Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
		bool swap = !oneModelMode && (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl));
		bool justRotate = Input.GetMouseButton(0);

		if (aroundZ)
		{
			float x = Input.GetAxis("Mouse X") ;
			tCur.RotateAround(light.forward, x / 2);
		}
		else if (rotateBothaAroundZ)
		{
			float x = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
			float y = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
			
			 
			Quaternion temp0 = t0.rotation;
			Quaternion temp1 = t1.rotation;
			if (Input.GetKey("space"))
				tAll.RotateAround(light.forward, x / 10);
			else
				tAll.Rotate(y, x, 0,  Space.World);

			 t0.rotation = temp0;
			 t1.rotation = temp1;
		}
		else if (swap)
		{
			SwapActiveModels();
		}
		else if (justRotate)
		{
			float x = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
			float y = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
			
			tCur.Rotate(y, x, 0,  Space.World);
			
			 rbCur.angularVelocity = Vector3.zero;
			 rbCur.AddTorque(y,x,0);
		}
	}//CheckInput

	private void SwapActiveModels()
	{
		firstActive = !firstActive;
		
		tCur.GetComponent<Model>().ToggleSelection(false);
		tCur = (firstActive) ? t0 : t1; 
		rbCur = (firstActive) ? rb0 : rb1;
		tCur.GetComponent<Model>().ToggleSelection(true);
		lvlController.soundController.PlayChangeModel();
	}

}
