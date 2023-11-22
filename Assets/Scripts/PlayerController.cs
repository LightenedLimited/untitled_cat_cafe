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
    public float initialAngle = -90f; 

    public Animator anim;

    private bool canpickup;
    private bool hasObject; 
    private GameObject pickupObject;
    private GameObject MouthLocation;
    private GameObject MouthReference;
    private Vector3 mouthDisplacement; 
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

        //MouthLocation = transform.GetChild(0).gameObject;
        MouthLocation = this.transform.Find("Armature.001/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Bone.005/Bone.038/MouthLocation").gameObject;
        //mouthDisplacement = MouthLocation.transform.position - MouthReference.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //mouthDisplacement
    }

    private void FixedUpdate()
    {
        Vector3 move = Quaternion.Euler(0, initialAngle, 0) * new Vector3(moveX, 0, moveY).normalized * velocity;
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
        desired_angle += (int)initialAngle; 
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
    private void AttachPickupObject()
    {
        pickupObject.GetComponent<Rigidbody>().isKinematic = true;
        pickupObject.GetComponent<BoxCollider>().enabled = false;
        pickupObject.transform.position = MouthLocation.transform.position; // sets the position of the object to your hand position
        pickupObject.transform.parent = MouthLocation.transform; //makes the object become a child of the parent so that it moves with the hands
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "PickupObject" && !hasObject) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            if(pickupObject != null)
            {
                pickupObject.GetComponent<InteractableController>().unHighlightInteractable();
            }
            pickupObject = other.gameObject; //set the gameobject you collided with to one you can reference
            pickupObject.GetComponent<InteractableController>().highlightInteractable(); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PickupObject")
        {
            canpickup = false;
            pickupObject.GetComponent<InteractableController>().unHighlightInteractable();
        }
    }
    void OnPickup()
    {
        //if (!canpickup) return;
        Debug.Log("INSIDE PICKUP"); 
        if(!hasObject && canpickup)
        {
            anim.SetTrigger("pickup"); 
            hasObject = true;
            Invoke("AttachPickupObject", 0.8f); 
        
        return; 
        }
        if(hasObject)
        {
            hasObject = false;
            pickupObject.GetComponent<Rigidbody>().isKinematic = false;
            pickupObject.transform.parent = null;
            pickupObject.GetComponent<BoxCollider>().enabled = true;
            return; 
        }

    }

    void OnJump() 
    {
        //rb.AddForce(jumpDirection * jumpMagnitude, ForceMode.Impulse);
        anim.SetTrigger("jump"); 
    }
}
