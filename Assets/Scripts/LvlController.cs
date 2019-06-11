using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class LvlController : ScriptableObject
{
    public ProgressController progressController;
    public LevelData data;
    public SoundController soundController;

    [HideInInspector] public ApplicationLifeCycle applicationLifeCycle;

    public bool testMode = false;

    void OnEnable()
	{
		Debug.Log("LvlController Enabled");
	}
    public void LoadLevel(LevelData data)
    {
        soundController.PlayGoToScene();
        this.data = data;
        Debug.Log("Load level");
        SceneManager.LoadScene(2);
    }

    public void LevelDone(float time)
    {
        float priv_time;

        soundController.PlayWin();
        if (progressController.records.doneLevels.TryGetValue(data.name, out priv_time))
        {
            if (time < priv_time)
                progressController.records.doneLevels[data.name] = time;
        }
        else
        {
            progressController.records.doneLevels.Add(data.name, time);
        }
        Debug.Log("Level done");
    }

    public void SetTestMode()
    {
        testMode = true;
    }

    public void GoToMenu()
    {
        soundController.PlayGoToScene();
        SceneManager.LoadScene(1);
    }

    public void GoToStartMenu()
    {
        testMode = false;
        soundController.PlayGoToScene();
        SceneManager.LoadScene(0);
    }
}
