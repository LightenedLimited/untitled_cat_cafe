using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance; 

    [SerializeField]
    GameObject taskContainer;

    [SerializeField]
    MenuManager menuManager;

    public int taskSize; 


    // Start is called before the first frame update
    private bool[] taskStatus;

    private int currentCoffees;
    public const int maxCoffees = 5;
    public const int coffeeTaskIndex = 0;

    private int childDodged;
    public const int maxChildDodged = 3;
    public const int childDodgedTaskIndex = 1;

    private bool stoveOnFire;
    public const int stoveOnFireTaskIndex = 2;

    private bool sleptOnLaptop;
    public const int sleptOnLaptopTaskIndex = 3; 
    private string[] taskCompleteStrings;

    // Update is called once per frame
    private void Awake()
    {
        taskCompleteStrings = new string[4]; //TODO: remove hard code
        taskCompleteStrings[0] = "<s>Knock Over 5 Coffees!</s>";
        taskCompleteStrings[1] = "<s>Dodge Child 3 Times!</s>";
        taskCompleteStrings[2] = "<s>FIRE!!!!!</s>";
        taskCompleteStrings[3] = "<s>Sleep on Laptop</s>";

        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 

        taskStatus = new bool[taskSize];
        for (int i = 0; i < taskSize; i++) taskStatus[i] = false;

        currentCoffees = 0;
        childDodged = 3;
        stoveOnFire = false;
        sleptOnLaptop = false; 
    }

    public void Update()
    {
        int i = 0;
        for(i = 0; i < taskSize; i++)
        {
            if (!taskStatus[i]) continue;

            Transform textObject = taskContainer.transform.GetChild(i);
            textObject.GetComponent<TMP_Text>().text = taskCompleteStrings[i]; 
        }
        if (taskStatus.Aggregate(true, (win, task) => win & task))
            menuManager.turnOnWinScreen();
    }

    public bool[] GetTaskStatus()
    {
        return taskStatus;
    }

    public int getCoffeesSpilled()
    {
        return currentCoffees; 
    }
    public void incrementCoffee()
    {
        if (currentCoffees >= maxCoffees) return;
        currentCoffees++;
        if (currentCoffees == maxCoffees) taskStatus[coffeeTaskIndex] = true; 
    }


    public int ChildDodged
    {
        get => childDodged;
        set { 
            childDodged = Math.Min(childDodged, Math.Clamp(value, 0, 3)); 
            if (childDodged == 0)
                taskStatus[childDodgedTaskIndex] = true;
        }
    }

    public bool getStoveOnFire()
    {
        return stoveOnFire; 
    }
    public void setStoveOnFire()
    {
        stoveOnFire = true;
        taskStatus[stoveOnFireTaskIndex] = true; 
    }
    public bool SleptOnLaptop {
        get => sleptOnLaptop; 
        set {
            sleptOnLaptop = sleptOnLaptop & value;
            taskStatus[sleptOnLaptopTaskIndex] = true;
        }
    }
}
