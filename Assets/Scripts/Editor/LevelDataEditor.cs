//  using UnityEngine;
//  using UnityEditor;
//  using System;

// [CustomEditor(typeof(LevelData))]
// public class LevelDataEditor : Editor
// {
//  public override void OnInspectorGUI()
//    {
// 	var myScript = target as LevelData;
// 	EditorUtility.SetDirty(myScript);
// 	myScript.mode = (LevelMode)EditorGUILayout.EnumPopup("Mode", myScript.mode);
	
// 	EditorGUILayout.Space();

// 	EditorGUILayout.PrefixLabel("Model 0");	
// 	myScript.model0 = (GameObject)EditorGUILayout.ObjectField(myScript.model0, typeof(GameObject));
// 	myScript.startRotation0 = EditorGUILayout.Vector3Field("Start Rotation 0" ,myScript.startRotation0);
// 	myScript.winRotation0 = EditorGUILayout.Vector3Field("Win Rotation 0" ,myScript.winRotation0);

	
// 	if(myScript.mode == LevelMode.TWO_MODELS)
// 	{
// 		EditorGUILayout.Space();

// 		EditorGUILayout.PrefixLabel("Model 1");	
// 		myScript.model1 = (GameObject)EditorGUILayout.ObjectField(myScript.model1, typeof(GameObject));
// 		myScript.startRotation1 = EditorGUILayout.Vector3Field("Start Rotation 1" ,myScript.startRotation1);
// 		myScript.winRotation1 = EditorGUILayout.Vector3Field("Win Rotation 1" , myScript.winRotation1);
		
// 		EditorGUILayout.Space();

// 		myScript.startRotationAll = EditorGUILayout.Vector3Field("Start Rotation All" ,myScript.startRotationAll);
// 		myScript.winRotationAll = EditorGUILayout.Vector3Field("Win Rotation All" , myScript.winRotationAll);
// 	}
//       //  
	

// 	//   using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(myScript.twoModelsMode)))
//     //     {
//     //         if (group.visible)
//     //         {
//     //             //EditorGUI.indentLevel++;
//     //     		myScript.mode = (LevelMode)EditorGUILayout.EnumPopup(myScript.mode);
//     //     	//	myScript.mode = (LevelMode)EditorGUILayout.EnumPopup

//     //              EditorGUILayout.PrefixLabel("Color");
//     //             EditorGUILayout.ColorField(Color.white);
//     //             // EditorGUILayout.PrefixLabel("Text");
//     //             // m_String = EditorGUILayout.TextField(m_String);
//     //             // EditorGUILayout.PrefixLabel("Number");
//     //             // m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
//     //             //EditorGUI.indentLevel--;

				
//     //         }
//     //     }
 
//    }
// }
