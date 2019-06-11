using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]
public class Selector : ScriptableObject
{
	/// <summary>
	/// Link to the current selected object - can be null
	/// </summary>
	public ISelectiable current;

	[HideInInspector] public UnityEvent onObjectSelectedEvent;
	[HideInInspector] public UnityEvent onObjectDeselectedEvent;

	/// <summary>
	/// Setting new selected object and update selection state of priv and current objects
	/// </summary>
	/// <param name="newSelected"></param>
	public void RegisterSelection (ISelectiable newSelected)
	{
		//TODO : Check if right state
		if (current != null)
					current.ToggleSelection (false);
		newSelected.ToggleSelection (true);
		current = newSelected;
		if (onObjectSelectedEvent != null)
			onObjectSelectedEvent.Invoke ();
		//Update state?
	}

	/// <summary>
	/// Will deselect current selected object
	/// </summary>
	/// <param name="touchedObj"></param>
	public void RemoveSelection (ISelectiable deselectMe)
	{
		//TODO : Check if right state
		deselectMe.ToggleSelection (false);
		current = null;
		if (onObjectDeselectedEvent != null)
			onObjectDeselectedEvent.Invoke ();
		//Change state?
	}

	// /// <summary>
	// /// Delete selected object
	// /// </summary>
	// public void DeleteCurrent ()
	// {
	// 	if (current != null)
	// 	{
	// 		Destroy (current.gameObject);
	// 		current = null;
	// 		if (onObjectDeselectedEvent != null)
	// 			onObjectDeselectedEvent.Invoke ();
	// 		//state?
	// 	}
	// }

}