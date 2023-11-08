using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    private float moveX, moveY;
    private Vector3 jumpDirection; 
    public float acceleration = 0.0f;
    public float jumpMagnitude = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        moveX = 0;
        moveY = 0;
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
        jumpDirection = new Vector3(0, 1, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveX, 0, moveY).normalized;
        rb.AddForce(move * acceleration);
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        moveX = v.x;
        moveY = v.y; 
    }

    void OnJump() 
    {
        rb.AddForce(jumpDirection * jumpMagnitude, ForceMode.Impulse);
    }
}
