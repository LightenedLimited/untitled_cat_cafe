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
        if (_value == 0) {
            masterMixer.SetFloat("MasterVolume", -100000);
            return; 
         }

        masterMixer.SetFloat("MasterVolume", Mathf.Log(_value) * 20f); 
    }

    public void MainVolumeControl(System.Single vol)
    {
        SetVolume(vol); 
    }

}
