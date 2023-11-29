using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool OnStove = false;
    private float doneness = 0f;
    public float Doneness => doneness;

    private Renderer renderer;
    private Color rawColor;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rawColor = renderer.material.color;
    }
    void Update()
    {
        if (OnStove) {
            doneness += Time.deltaTime/30;
            renderer.material.SetColor("_Color", Color.Lerp(rawColor, Color.black, doneness));
        }
    }
}