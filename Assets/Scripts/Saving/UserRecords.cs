using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class UserRecords
{
    public AppSetting appSettings;
    public Dictionary<string, float> doneLevels;

    public UserRecords()
    {
        doneLevels = new Dictionary<string, float>();
        appSettings = new AppSetting();
    }
}

[System.Serializable]
public class AppSetting
{
    public bool fullscreen = false;
    [Range(0,1)]
    public float soundVolume = 0.5f;
    [Range(0,1)]
    public float musicVolume = 0.5f;
    public float sensetivity;

}