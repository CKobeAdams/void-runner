using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    private GameObject groundCheck;

    [SerializeField]
    private Camera MainCam;

    [SerializeField]
    private float movementSpeed, acceleration, gravityAccel = 10f, maxMovementSpeed = 5f, currentVelocity = 0f, moveDirection, jumpVelocity = 5f, jumpCancelAcel = 5f;
    public bool isMoving, isGrounded, gravityAffected, jumpCancelled;
    private int groundLayer = 7;
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 100f;
        gravityAffected = true;
        
        rigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        moveVector = new Vector2(0f,0f);
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gravityAffected && !isGrounded)
        {
            //rigidbody.velocity  = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y-gravityAccel*Time.deltaTime);
            
        }

        rigidbody.velocity = new Vector2(maxMovementSpeed * moveVector.x, rigidbody.velocity.y);
        MainCam.transform.position = new Vector3(this.transform.position.x, MainCam.transform.position.y, MainCam.transform.position.z);
        //isGrounded = Physics.CheckSphere(groundCheck.transform.position,.2f,lay)

        if(rigidbody.velocity.y < 0.75f*jumpVelocity || jumpCancelled)
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * Time.fixedDeltaTime * jumpCancelAcel;
        }

    }

    public void MovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = new Vector2(context.ReadValue<float>(), moveVector.y);
        

    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GroundCheck();
        }
        if(context.performed && isGrounded)
        {
            //crouch, slide, and then walk at a crouched movement speed
           
            //quarternions are required for rotations

            this.transform.rotation = new Quaternion(90, 0, 0, 0);
        }

        if(context.canceled)
        {
            //stand up
            //this.transform.rotation = new Vector3(0, 0, 0);
            //quarternions is required to return the rotation
        }
    }

    public void GroundCheck()
    {

        Debug.Log(groundCheck.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")));
        if(groundCheck.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true; 
        }
        else
        {
            isGrounded = false;
        }

    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(context.started)
        {
            jumpCancelled = false;
        }

        if(context.performed)
        {
            GroundCheck();
            if(isGrounded)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
            }
        }

        if(context.canceled)
        {
            jumpCancelled = true;
        }
    }
}
