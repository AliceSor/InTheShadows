using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	/// <summary>
	/// Change material if model active
	/// </summary>
public class Model : MonoBehaviour
{
	public Material normalMat;
	public Material highligtedMat;

	private MeshRenderer renderer;
	private Material[] materials;

	void Start()
	{
		renderer = GetComponent<MeshRenderer>();
		materials = renderer.sharedMaterials;
	}

	public void ToggleSelection(bool selection)
	{
		if (renderer == null)
			renderer = GetComponent<MeshRenderer>();

		if (selection)
		{
			materials[0] = highligtedMat;
			renderer.sharedMaterials = materials;
		}
		else
		{
			materials[0] = normalMat;
			renderer.sharedMaterials = materials;
		}
	}//ToggleSelection
}
