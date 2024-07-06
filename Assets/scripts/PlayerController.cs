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
    private float movementSpeed, acceleration, gravityAccel = 10f, maxMovementSpeed, currentVelocity, moveDirection, jumpVelocity = 5f;
    public bool isMoving, isGrounded, gravityAffected;
    private int groundLayer = 7;
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 100f;
        gravityAffected = true;
        maxMovementSpeed = 5f;
        currentVelocity = 0;
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

    }

    public void MovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = new Vector2(context.ReadValue<float>(), moveVector.y);
        

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
        if(context.performed)
        {
            GroundCheck();
            if(isGrounded)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
            }
        }
    }
}
