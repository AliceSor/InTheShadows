using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientSound : MonoBehaviour
{
	[SerializeField] SoundController controller;
	/*[SerializeField]*/ AudioSource audioSource;

	private void Start()
	{
		if (controller == null)
			Destroy(this);
		audioSource = GetComponent<AudioSource>();
		controller.musicVolumeChanged += OnVolumeChanged;
		audioSource.clip = controller.ambientCollection[Random.Range(0, controller.ambientCollection.Length)];
		audioSource.Play();
	}

	public void OnVolumeChanged(float volume)
	{
		audioSource.volume = volume;
	}

	void OnDestroy()
	{
		if (controller != null)
			controller.musicVolumeChanged -= OnVolumeChanged;
	}
}
