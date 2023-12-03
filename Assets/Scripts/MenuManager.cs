using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MenuManager : MonoBehaviour
{
    public GameObject taskManagerObject; 

    public Canvas pauseScreen;
    public Canvas taskScreen;

    private TaskManager taskManager; 

    private string[] taskCompleteStrings; 

    // Update is called once per frame
    private void Start()
    {
        pauseScreen.enabled = false;
        taskScreen.enabled = false;
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

            Transform textObject = taskScreen.transform.Find("Tasks").GetChild(i);
            textObject.GetComponent<TMP_Text>().text = taskCompleteStrings[i]; 
        }

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

    
}
