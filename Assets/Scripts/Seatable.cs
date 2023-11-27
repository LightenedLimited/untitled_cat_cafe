using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seatable : MonoBehaviour
{
    public enum SeatType {Cushion, Booth};
    [SerializeField] public Vector3 SeatOffset;
    public bool occupied = false;
    // private GameObject occupant = false;

    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }
}
