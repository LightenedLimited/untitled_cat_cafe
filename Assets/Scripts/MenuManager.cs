using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject taskManagerObject; 

    public Canvas pauseScreen;
    public Canvas taskScreen;
    public Canvas optionsScreen;
    public Canvas controlScreen; 
    public Canvas winScreen; 

    [SerializeField]
    GameObject taskContainer;

    [SerializeField]
    AudioSource musicSource;


    private TaskManager taskManager; 

    private string[] taskCompleteStrings;

    public static MenuManager Instance;

    private SettingsManager settingsManager;

    // Update is called once per frame
    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this; 
        DontDestroyOnLoad(gameObject);

        pauseScreen.enabled = false;
        taskScreen.enabled = false;
        optionsScreen.enabled = false;
        controlScreen.enabled = false; 
        winScreen.enabled = false;

        settingsManager = GameObject.Find("SettingsManager").GetComponent<SettingsManager>(); 
    }

    private void Update()
    {
        taskManager = taskManagerObject.GetComponent<TaskManager>();
    }

    public void turnOnPaused()
    {
        pauseScreen.enabled = true;
        GameManager.Instance.UpdateGameState(GameState.GamePaused); 
        musicSource.Pause();
    }

    public void turnOffPaused()
    {
        pauseScreen.enabled = false;
        optionsScreen.enabled = false;
        controlScreen.enabled = false;
        GameManager.Instance.UpdateGameState(GameState.GameRunning);
        musicSource.UnPause();
    }


    public void turnOnTaskManager()
    {
        taskScreen.enabled = true;
        // GameManager.Instance.UpdateGameState(GameState.GamePaused);
    }
    public void turnOffTaskManager()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false;
        optionsScreen.enabled = false;
        controlScreen.enabled = false;
        // GameManager.Instance.UpdateGameState(GameState.GameRunning);
    }

    public void turnOnOptions()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false;
        optionsScreen.enabled = true;
        controlScreen.enabled = false; 
    }

    public void turnOnControls()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false;
        optionsScreen.enabled = false;
        controlScreen.enabled = true;
    }

    public void turnOnWinScreen()
    {
        winScreen.enabled = true;
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("Scenes/Main Menu");
    }

    public void MainVolumeControl(System.Single vol)
    {
        settingsManager.SetVolume(vol); 
    }
}
