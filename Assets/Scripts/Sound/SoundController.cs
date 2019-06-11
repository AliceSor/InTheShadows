using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class SoundController : ScriptableObject
{
	[Range(0,1)]
	public float soundVolume;
	[Range(0,1)]
	public float musicVolume;

	public AudioClip[] ambientCollection;
	public AudioClip uiClick;
	public AudioClip winSound;
	public AudioClip goToAnotherScene;
	public AudioClip changeModel;

	public delegate void PlaySound(AudioClip clip);

	public PlaySound onPlayUIClick;
	public PlaySound onPlayWin;
	public PlaySound onPlayGoToScene;
	public PlaySound onPlayChangeModel;

	public delegate void VolumeChanged(float newVolume);
	public VolumeChanged soundVolumeChanged;
	public VolumeChanged musicVolumeChanged;

	void OnEnable()
	{
		Debug.Log("SoundController Enabled");
	}

	public void PlayUIClick()
    {
		if (onPlayUIClick != null)
			onPlayUIClick(uiClick);
    }
	public void PlayWin()
    {
		if (onPlayWin != null)
			onPlayWin(winSound);
    }
	public void PlayGoToScene()
    {
		if (onPlayGoToScene != null)
			onPlayGoToScene(goToAnotherScene);
    }
	public void PlayChangeModel()
    {
		if (onPlayChangeModel != null)
			onPlayChangeModel(changeModel);
    }

	// public void SetSoundVolume(float volume)
    // {
	// 	soundVolume = volume;
	// 	if (soundVolumeChanged != null)
	// 		soundVolumeChanged(volume);
    // }

	public void SetSoundVolume(Slider volumeSlider)
    {
		soundVolume = volumeSlider.value;
		if (soundVolumeChanged != null)
			soundVolumeChanged(soundVolume);
    }

    // public void SetMusicVolume(float volume)
    // {
    //     musicVolume = volume;
	// 	if (musicVolumeChanged != null)
	// 		musicVolumeChanged(volume);
    // }

	public void SetMusicVolume(Slider volumeSlider)
    {
        musicVolume = volumeSlider.value;
		if (musicVolumeChanged != null)
			musicVolumeChanged(musicVolume);
    }
}
