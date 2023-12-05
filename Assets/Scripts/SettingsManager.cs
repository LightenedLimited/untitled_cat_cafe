using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    private float volume; 

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        if(SettingsManager.Instance != null)
        {
            MainVolumeControl(0.5f);
        }
    }
    public void MainVolumeControl(System.Single vol)
    {
        SettingsManager.Instance.volume = vol;
    }

}
