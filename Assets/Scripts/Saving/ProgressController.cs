using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu()]
public class ProgressController : ScriptableObject
{
    public string filename = "userRecords";
    public UserRecords records;
    public SoundController soundController;

    private bool firstLoad = true;

    void OnEnable()
	{
		Debug.Log("ProgressController Enabled");
        if (firstLoad)
        {
            LoadProgres();
            firstLoad = false;
        }
        if (records != null && soundController != null)
        {
            Screen.fullScreen = records.appSettings.fullscreen;
            soundController.soundVolume = records.appSettings.soundVolume;
            soundController.musicVolume = records.appSettings.musicVolume;
            soundController.soundVolumeChanged += OnVolumeChanged;
            soundController.musicVolumeChanged += OnVolumeChanged;
        }
	}

    void OnDestroy()
    {
        Debug.Log("ProgressController Destroyed");
        if (records != null && soundController != null)
        {
            Screen.fullScreen = records.appSettings.fullscreen;
            soundController.soundVolume = records.appSettings.soundVolume;
            soundController.musicVolume = records.appSettings.musicVolume;
        }
        SaveProgres();
    }

    public void SetFullscreen(Toggle isFullScreen)
    {
        
        Screen.fullScreen = isFullScreen.isOn;
        records.appSettings.fullscreen = isFullScreen.isOn;
        Debug.Log("FullScreen now " +  records.appSettings.fullscreen);
    }

    public void OnVolumeChanged(float volume)
	{
        records.appSettings.soundVolume = soundController.soundVolume;
        records.appSettings.musicVolume = soundController.musicVolume;
         Debug.Log("sound volume now " +  records.appSettings.soundVolume);
	}

    public  void SaveProgres()
    {
        Debug.Log("Closing... fullscreen : " + records.appSettings.fullscreen);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create (Application.persistentDataPath + filename); //you can call it anything you want
        bf.Serialize(file, records);
        file.Close();
    }   
     
    public  void LoadProgres()
    {
        if(File.Exists(Application.persistentDataPath + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
            records = (UserRecords)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            records = new UserRecords();
            records.appSettings.fullscreen = Screen.fullScreen;
        }
    }

    public void DeleteProgress()
    {
        if(File.Exists(Application.persistentDataPath + filename))
        {
            File.Delete(Application.persistentDataPath + filename);
            records = new UserRecords();
        }
    }

    public void CloseApp()
    {
        SaveProgres();
        Application.Quit();
    }
}//ProgressController
