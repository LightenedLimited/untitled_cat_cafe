using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private GameObject occupant;

    public bool Occupied => occupant is not null;
    public GameObject Occupant => occupant;

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
    public void Leave()
    {
        occupant = null;
    }
}
