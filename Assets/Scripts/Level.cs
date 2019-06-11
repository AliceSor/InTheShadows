using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum LevelState {LOCKED, UNLOCKED, DONE}
public class Level : MonoBehaviour, ISelectiable
{
	public int number;

	/// <summary>
	/// State before first game start without concerning about game progress
	/// </summary>
	public LevelState defaultState;
	
	public bool beingDragged = false;

	public Level nextLevel;
	public float rotationSpeed;

	public LevelData data;
	public Material normalMat;
	public Material highligtedMat;

	public TextMesh lvlNameText;
	public TextMesh lvlTimeText;
	public LvlController lvlController;
	public Selector selector;
	public UnityEvent onLevelChangeState;

	public LevelState state
	{
		get
		{
			return _state;
		}
	}

	private Vector3 startPosition;
	private Rigidbody rb;
	private bool selected = false;
	private MeshRenderer renderer;
	private Material[] materials;
	private int selectionCount = 0;
	private LevelState _state;

	IEnumerator Start()
	{
		if (data == null)
			Destroy(this);
		name = data.name;
		startPosition = transform.position;
		rb = GetComponent<Rigidbody>();
		renderer = GetComponent<MeshRenderer>();
		materials = renderer.sharedMaterials;
		lvlNameText.text = data.name;
		_state = defaultState;
		yield return null;
		CheckRecords();

		if (state == LevelState.LOCKED && lvlController.testMode)
			ChangeState(LevelState.UNLOCKED);

	}

	public void LevelSelected()
	{
		//LvlController.instance.LoadLevel(data);
		lvlController.LoadLevel(data);
	}

	void CheckRecords()
	{
		if (data != null && lvlController.progressController.records.doneLevels.ContainsKey(data.name))
		{
			lvlTimeText.text = "Time : " + lvlController.progressController.records.doneLevels[data.name];
			ChangeState(LevelState.DONE);
			if (nextLevel != null)
			{
				if (nextLevel.state == LevelState.LOCKED)
					nextLevel.ChangeState(LevelState.UNLOCKED);
			}
		}
	}

	public void ChangeState(LevelState newState)
	{
		if (_state != newState && data != null)
		{
			_state = newState;
			Debug.Log("Level " + data.name + " become" + newState.ToString());
			if (onLevelChangeState != null)
				onLevelChangeState.Invoke();
		}
	}

	public void ToggleSelection(bool selection)
	{
		if (renderer == null)
			renderer = GetComponent<MeshRenderer>();
		selected = selection;
		if (selected)
		{
			materials[0] = highligtedMat;
			renderer.sharedMaterials = materials;
			lvlNameText.gameObject.SetActive(true);
			if (state == LevelState.DONE)
				{
					lvlTimeText.gameObject.SetActive(true);
				}
		}
		else
		{
			materials[0] = normalMat;
			renderer.sharedMaterials = materials;
			lvlNameText.gameObject.SetActive(false);
			if (state == LevelState.DONE)
				lvlTimeText.gameObject.SetActive(false);
		}
	}

	void OnMouseDrag()
	{
		beingDragged = true;
		float x = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed * 10;
		

			
			//transform.Rotate(x, 0, 0,  Space.World);
			
			 rb.angularVelocity = Vector3.zero;
			 rb.AddTorque(x,0,0);

	}

	Vector3 pos;

	private void OnMouseDown()
	{
		Debug.Log("cllik");
		pos = Input.mousePosition;
		selector.RegisterSelection(this);
		
	}
	
	void OnMouseUp()
	{
		if (Vector3.Distance(pos, Input.mousePosition) <= 2)
		{
			Debug.Log("cllik no drug");
			if (selected && selectionCount > 0 && state != LevelState.LOCKED)
			{
				LevelSelected();
				selectionCount = 0;
			}
			else
			{
				selector.RegisterSelection(this);
				selectionCount ++;
			}
		}
		else if (beingDragged)
		{
			if (selected)
				selector.RemoveSelection(this);
			beingDragged = false;
			
		}
	}
}
