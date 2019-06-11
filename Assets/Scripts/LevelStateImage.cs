using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateImage : MonoBehaviour
{
	public Material lockedMat;
	public Material unlockedMat;
	public Material doneMat;

	public Level level;

	private LevelState currentState = LevelState.UNLOCKED;
	private MeshRenderer renderer;
	private Material[] materials;


	IEnumerator Start()
	{
		renderer = GetComponent<MeshRenderer>();
		materials = renderer.sharedMaterials;
		
		level.onLevelChangeState.AddListener(SetMaterial);
		yield return null;
		if (currentState != level.state)
			SetMaterial();
	}

	public void SetMaterial()
	{
		switch (level.state)
		{
			case LevelState.UNLOCKED:
			{
				materials[0] = unlockedMat;
				break;
			}
			case LevelState.LOCKED:
			{
				materials[0] = lockedMat;
				break;
			}
			case LevelState.DONE:
			{
				materials[0] = doneMat;
				break;
			}
		}
		
		renderer.sharedMaterials = materials;
	}

}
