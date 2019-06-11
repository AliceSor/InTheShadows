using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum LevelMode {ONE_MODEL, TWO_MODELS}

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
	public string name;
	public LevelMode mode = LevelMode.ONE_MODEL;
//	public bool twoModelsMode = false;
	
	[Header("Model1")]
	public GameObject model0;
	public float precision0;
	public Vector3 startRotation0;
	public Vector3 winRotation0;
	public Vector3 winRotation0_1;

	[Header("Model1")]
	public GameObject model1;
	public float precision1;
	public Vector3 startRotation1;
	public Vector3 winRotation1;
	public Vector3 winRotation1_1;

	[Header("Respective models rotation")]
//	public Vector3 startRotationAll;
	public float precisionAll;
	public Vector3 winRotationAll;
	public Vector3 winRotationAll_1;
}

