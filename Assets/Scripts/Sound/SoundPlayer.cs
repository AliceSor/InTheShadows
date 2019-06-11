using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Receive events from SoundController and play
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
	[SerializeField] SoundController controller;
	/*[SerializeField]*/ AudioSource audioSource;
	
	private void Start()
	{
		if (controller == null)
			Destroy(this);
		audioSource = GetComponent<AudioSource>();
		controller.soundVolumeChanged += OnVolumeChanged;
		controller.onPlayUIClick += OnUIClick;
		controller.onPlayWin += OnWin;
		controller.onPlayChangeModel += OnModelChanged;
		controller.onPlayGoToScene += OnSceneChanged;
	}

	public void OnVolumeChanged(float volume)
	{
		audioSource.volume = volume;
	}

	public void OnUIClick(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
	public void OnWin(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
	public void OnSceneChanged(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
	public void OnModelChanged(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}

	void OnDestroy()
	{
		if (controller != null)
		{
			controller.musicVolumeChanged -= OnVolumeChanged;
			controller.onPlayUIClick -= OnUIClick;
			controller.onPlayWin -= OnWin;
			controller.onPlayChangeModel -= OnModelChanged;
			controller.onPlayGoToScene -= OnSceneChanged;
		}
	}
	

}
