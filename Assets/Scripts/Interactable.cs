using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum Status { Usable, Used, Unusuable };
    private Status _status;
    public Status status
    {
        get { return _status; }
        set
        {
            _status = value;
            UpdateColor();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        // if tips, update to unusable
        if (Vector3.Angle(gameObject.transform.up, Vector3.up) > 45)
        {
            status = Status.Unusuable;
        }
    }

    public bool UseItem()
    {
        status = Status.Used;
        return true;
    }

    // update color of item
    private void UpdateColor()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        if (renderer is not null)
        {
            switch (status)
            {
                case Status.Unusuable:
                    renderer.material.SetColor("_Color", Color.red);
                    break;
                case Status.Usable:
                    renderer.material.SetColor("_Color", Color.green);
                    break;
                case Status.Used:
                    renderer.material.SetColor("_Color", Color.gray);
                    break;
            }
        }

    }
}
