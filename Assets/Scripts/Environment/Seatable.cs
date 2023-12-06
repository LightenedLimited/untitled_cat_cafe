using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seatable : MonoBehaviour
{
    public enum SeatType { Cushion, Booth };
    [SerializeField] public Vector3 SeatOffset;
    [SerializeField] public SeatType Type;
    private GameObject occupant;

    public Vector3 SeatPosition => transform.position + SeatOffset;
    public Quaternion SeatRotation => gameObject.transform.rotation;
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
