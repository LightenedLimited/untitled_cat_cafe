using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance; 

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

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Awake()
    {
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
        childDodged = 0;
        stoveOnFire = false;
        sleptOnLaptop = false; 
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


    public int getChildDodged()
    {
        return childDodged; 
    }

    public void incrementChildDodged()
    {
        if (childDodged >= maxChildDodged) return;
        childDodged++;
        if (childDodged == maxChildDodged) taskStatus[childDodgedTaskIndex] = true; 
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
    public bool getSleptOnLaptop()
    {
        return sleptOnLaptop;
    }
    public void setSleptOnLaptop()
    {
        sleptOnLaptop = true;
        taskStatus[sleptOnLaptopTaskIndex] = true;
    }

}
