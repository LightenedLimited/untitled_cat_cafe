using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    private float moveX, moveY;
    private Vector3 jumpDirection; 
    public float velocity = 0.0f;
    public float jumpMagnitude = 0.0f;
    public float rotation_speed = 0.001f;
    float timeCount = 0.0f;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        moveX = 0;
        moveY = 0;
        rb = transform.GetComponent<Rigidbody>();
        jumpDirection = new Vector3(0, 1, 0);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveX, 0, moveY).normalized * velocity;
        Debug.Log(move); 
        rb.velocity = move; 

        if (moveX != 0 || moveY != 0)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        Quaternion current = transform.rotation;
        int desired_angle = 0;
        if (moveX > 0 && moveY > 0) {
            desired_angle = 45;
         }
        if (moveX == 0 && moveY > 0) desired_angle = 0;
        if (moveX < 0 && moveY > 0) desired_angle = -45;
        if (moveX < 0 && moveY == 0) desired_angle = -90;
        if (moveX > 0 && moveY == 0) desired_angle = 90;
        if (moveX < 0 && moveY < 0) desired_angle = -135;
        if (moveX == 0 && moveY < 0) desired_angle = 180;
        if (moveX > 0 && moveY < 0) desired_angle = 135;

        Quaternion desired_rotation = Quaternion.Euler(0, desired_angle, 0); 
        if (moveX == 0 && moveY == 0)
        {
            desired_rotation = current; 
        }
        transform.rotation = Quaternion.Lerp(current, desired_rotation, Time.deltaTime * rotation_speed);
        timeCount += Time.deltaTime; 


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
