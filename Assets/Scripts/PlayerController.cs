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
    public float jumpXZMagnitude = 4f;
    public float rotation_speed = 2f;
    float timeCount = 0.0f;
    public float initialAngle = -90f;
    public float jumpThreshold = 10f;
    public float catHeight = 2f;
    public float jumpAnimationTime = 1.5f; 

    public Animator anim;

    private bool canpickup;
    private bool duringJump; 
    private bool hasObject;

    private bool inJumpRange; 

    private GameObject pickupObject;
    private GameObject MouthLocation;
    private Vector3 mouthDisplacement;

    private GameObject jumpObject;

    private Vector3 desiredJumpLocation; 

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

        inJumpRange = false;
        duringJump = false; 

        MouthLocation = this.transform.Find("Armature.001/Bone/Bone.001/Bone.002/Bone.003/Bone.004/Bone.005/Bone.038/MouthLocation").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(duringJump)
        {

        }
    }

    private void FixedUpdate()
    {
        if (duringJump) return; 

        Vector3 move = Quaternion.Euler(0, initialAngle, 0) * new Vector3(moveX, 0, moveY).normalized * velocity;
        move += new Vector3(0, rb.velocity.y, 0); 
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
        if(other.gameObject.tag == "JumpEntry")
        {

            inJumpRange = true; 
            jumpObject = other.gameObject; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PickupObject")
        {
            canpickup = false;
            pickupObject.GetComponent<InteractableController>().unHighlightInteractable();
        }
        if(other.gameObject.tag == "JumpEntry")
        {
            inJumpRange = false;
        }
    }
    void OnPickup()
    {
        //if (!canpickup) return;
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
        float gravity = Physics.gravity.y; 
        if (!inJumpRange || duringJump) return;
        float angle = Vector3.Angle(this.transform.forward, (jumpObject.transform.position - this.transform.position).normalized); 
        if (angle > jumpThreshold) return;
        //disable movement during jump? 
        //anim.SetTrigger("jump");
        Debug.Log("inside"); 
        Vector3 xz_velocity = this.transform.forward.normalized * jumpXZMagnitude / jumpAnimationTime;
        float height = jumpObject.transform.position.y - this.transform.position.y;
        Vector3 y_velocity = new Vector3(0, (height / jumpAnimationTime - 0.5f * gravity * jumpAnimationTime), 0);
        Debug.Log(xz_velocity + y_velocity); 
        rb.velocity = xz_velocity + y_velocity; 
        duringJump = true;
        Invoke("jumpEnd", jumpAnimationTime); 
    }

    public void jumpEnd()
    {
        //float new_x = this.transform.position.x; 
        //float new_y = jumpObject.transform.position.y - catHeight;
        //float new_z = this.transform.position.z;
        //desiredJumpLocation = new Vector3(new_x, new_y, new_z) + this.transform.forward.normalized * jumpXZMagnitude; 
        //Debug.Log(new Vector3(new_x, new_y, new_z) + this.transform.forward.normalized * jumpXZMagnitude); 
        //this.transform.position = new Vector3(new_x, new_y, new_z) + this.transform.forward.normalized * jumpXZMagnitude;
        duringJump = false; 
    }
}
