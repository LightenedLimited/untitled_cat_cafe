using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public float volume;

    [SerializeField] AudioMixer masterMixer; 

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

    public void SetVolume(float _value)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log(_value) * 20f); 
    }

    public void MainVolumeControl(System.Single vol)
    {
        SetVolume(vol); 
    }

}
