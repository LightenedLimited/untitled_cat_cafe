using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    private float moveX, moveY;
    private Vector3 jumpDirection; 
    public float velocity = 5f;
    public float jumpMagnitude = 0.0f;
    public float rotation_speed = 2f;
    float timeCount = 0.0f;

    public Animator anim;

    private bool canpickup;
    private bool hasObject; 
    private GameObject pickupObject; 
    // Start is called before the first frame update
    void Start()
    {
        moveX = 0;
        moveY = 0;
        rb = transform.GetComponent<Rigidbody>();
        jumpDirection = new Vector3(0, 1, 0);

        anim = GetComponent<Animator>();
        canpickup = false;
        hasObject = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveX, 0, moveY).normalized * velocity;
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
        desired_angle *= -1; 
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
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        Debug.Log(other.gameObject.tag); 
        if (other.gameObject.tag == "PickupObject") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            pickupObject = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Mug")
        {
            canpickup = false; 
        }
    }
    void OnPickup()
    {
        if (!canpickup) return;
        FixedJoint joint = pickupObject.GetComponent <FixedJoint>();
        Debug.Log("INSIDE PICKUP"); 
        if(!hasObject)
        {
            hasObject = true;
            joint.connectedBody = rb;
            Debug.Log("connected joint"); 
            return; 
        }
        if(hasObject)
        {
            hasObject = false;
            joint.connectedBody = null;
            return; 
        }

    }

    void OnJump() 
    {
        rb.AddForce(jumpDirection * jumpMagnitude, ForceMode.Impulse);
    }
}
