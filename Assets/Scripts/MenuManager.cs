using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MenuManager : MonoBehaviour
{
    public GameObject taskManagerObject; 

    public Canvas pauseScreen;
    public Canvas taskScreen;
    public Canvas optionsScreen;
    public Canvas controlScreen; 

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


        taskCompleteStrings = new string[4]; //TODO: remove hard code
        taskCompleteStrings[0] = "<s>Knock Over 5 Coffees!</s>";
        taskCompleteStrings[1] = "<s>Dodge Child 3 Times!</s>";
        taskCompleteStrings[2] = "<s>Set Stove on Fire</s>";
        taskCompleteStrings[3] = "<s>Sleep on Laptop</s>";
    }

    private void Update()
    {
        taskManager = taskManagerObject.GetComponent<TaskManager>();
        bool[] taskStatus = taskManager.GetTaskStatus();
        for(int i = 0; i < taskManager.taskSize; i++)
        {
            if (!taskStatus[i]) continue;

            Transform textObject = taskContainer.transform.GetChild(i);
            textObject.GetComponent<TMP_Text>().text = taskCompleteStrings[i]; 
        }

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
        //GameManager.Instance.UpdateGameState(GameState.GamePaused);
    }
    public void turnOffTaskManger()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false;
        optionsScreen.enabled = false;
        controlScreen.enabled = false;
        //GameManager.Instance.UpdateGameState(GameState.GameRunning);
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

    public void MainVolumeControl(System.Single vol)
    {
        SettingsManager.Instance.volume = vol;
    }
}
