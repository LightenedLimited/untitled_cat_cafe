using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxDanger = 50;
    [SerializeField] private float baseDangerRate = 2;
    public float MaxDanger => maxDanger;
    public bool OnFire => danger >= maxDanger;
    private int numFood = 0;
    public int NumFood => numFood;
    private float danger = 0;
    private float timer = 0;
    private Renderer renderer;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Food") &&
            coll.gameObject.transform.position.y >= gameObject.transform.position.y)
        {
            numFood++;
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("Food") &&
            coll.gameObject.transform.position.y >= gameObject.transform.position.y)
        {
            numFood--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer -= 1f;
            danger += baseDangerRate + numFood;
            UpdateColor();
        }
    }
    public void Use()
    {
        danger /= 2;
        danger -= 3;
        UpdateColor();
    }

    private void UpdateColor()
    {
        renderer.material.SetColor("_Color", Color.Lerp(Color.white, Color.red, danger/maxDanger));
    }
}
