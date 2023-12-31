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
    public float jumpXZMultiplier = 1.2f;
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
        pickupObject.GetComponent<Collider>().enabled = false;
        pickupObject.transform.position = MouthLocation.transform.position; // sets the position of the object to your hand position
        pickupObject.transform.parent = MouthLocation.transform; //makes the object become a child of the parent so that it moves with the hands
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.tag == "Laptop")
        {
            Debug.Log("touching Laptop");
            Laptop laptop = null;
            if (other.gameObject.TryGetComponent(out laptop))
                laptop.TrySit(gameObject);
        }
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
        if (!inJumpRange || duringJump) return;
        //float angle = Vector3.Angle(this.transform.forward, (jumpObject.transform.position - this.transform.position).normalized);
        Vector2 jumpObjectPos = new Vector2(jumpObject.transform.position.x, jumpObject.transform.position.z);
        Vector2 thisObjectPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 forwardVector = new Vector2(this.transform.forward.x, this.transform.forward.z); 
        float angle = Vector2.Angle(forwardVector, (jumpObjectPos - thisObjectPos).normalized);
        Debug.Log(angle); 
        Debug.Log((jumpObjectPos - thisObjectPos).normalized);
        if (angle > jumpThreshold) return;
        //disable movement during jump? 
        anim.SetTrigger("jump");
        duringJump = true;
    }

    public void ApplyJumpVelocity()
    {
        float gravity = Physics.gravity.y;
        Vector3 displacement = jumpObject.transform.position - this.transform.position;
        Vector3 xz_velocity = new Vector3(displacement.x / jumpAnimationTime * jumpXZMultiplier, 0, displacement.z / jumpAnimationTime * jumpXZMultiplier); 
        float height = jumpObject.transform.position.y - this.transform.position.y + catHeight;
        Vector3 y_velocity = new Vector3(0, (height / jumpAnimationTime - 0.5f * gravity * jumpAnimationTime), 0);
        Debug.Log(height); 
        rb.velocity = xz_velocity + y_velocity;
        
        turnOffCollision();
    }

    public void jumpEnd()
    {
        duringJump = false;
    }
    public void turnOnCollision()
    {
        GetComponent<BoxCollider>().enabled = true;    
    }
    public void turnOffCollision()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Yeet()
    {
        Yeet(new Vector3(0, 5, 0));
    }
    public void Yeet(Vector3 vel)
    {
        IEnumerator yeetCoroutine = Yote(vel);
        StartCoroutine(yeetCoroutine);
    }
    private IEnumerator Yote (Vector3 vel)
    {
        rb.velocity = vel;
        yield return new WaitForSeconds(2);
        
    }
}
