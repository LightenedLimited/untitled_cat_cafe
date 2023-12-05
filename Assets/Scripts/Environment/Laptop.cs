using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private GameObject occupant;

    public bool Occupied => occupant is not null;
    public GameObject Occupant => occupant;
    public float SitRequirement = 10f;
    private float sitTimer = 0;

    public bool TrySit(GameObject g)
    {
        if (!Occupied)
        {
            occupant = g;
            return true;
        }
        else
            return false;
    }
    public void Eject()
    {
        occupant = null;
    }

    public void Update()
    {
        if (occupant is not null)
        {
            if (occupant.CompareTag("Player")) 
            {
                sitTimer += Time.deltaTime;
                if (sitTimer >= SitRequirement)
                    TaskManager.Instance.SleptOnLaptop = true;
            }
            if (Vector3.Distance(occupant.transform.position, transform.position) >= 2)
                Eject();
        } 
        else sitTimer = 0;
    }
}
