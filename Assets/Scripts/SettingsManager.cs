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
            setVolume(0.5f);
        }
    }

    public void setVolume(float newVolume)
    {
        SettingsManager.Instance.volume = newVolume; 
    }

}
