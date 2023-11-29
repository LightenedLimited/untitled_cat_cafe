using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas pauseScreen;
    public Canvas taskScreen;

    // Update is called once per frame
    private void Start()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false; 
    }


    public void turnOnPaused()
    {
        pauseScreen.enabled = true; 
    }

    public void turnOffPaused()
    {
        pauseScreen.enabled = false;
    }


    public void turnOnTaskManager()
    {
        taskScreen.enabled = true; 
    }
    public void turnOffTaskManger()
    {
        taskScreen.enabled = false; 
    }

    public void StartGame()
    {
         SceneManager.LoadScene("Scenes/Cafe");
    }
}
