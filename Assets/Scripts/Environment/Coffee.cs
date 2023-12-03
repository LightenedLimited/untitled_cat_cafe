using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coffee : MonoBehaviour
{

    // Should be between 0 and 1
    [SerializeField] public float tipAngle = 45f;
    [SerializeField] public float amount = 1.0f;
    // // Start is called before the first frame update
    private Renderer renderer;
    public float Amount
    {
        get { return amount; }
        set
        {
            // amount = value;
            amount = Math.Clamp(value, 0, 1);
            UpdateColor();
        }
    }
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        // if tips, update to unusable
        if (Vector3.Angle(gameObject.transform.up, Vector3.up) > tipAngle)
        {
            Amount = 0;
            TaskManager.Instance.incrementCoffee(); 
        }
    }

    // // update color of item
    private void UpdateColor()
    {
        renderer.material.SetColor("_Color", Color.Lerp(Color.red, Color.green, amount));
    }
}
