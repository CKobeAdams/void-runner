using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    private Rigidbody2D rigidbody;

    [SerializeField]
    private GameObject groundCheck, flipHitBox, stumbleBox;

    [SerializeField]
    private Camera MainCam;

    [SerializeField]
    private float CameraFloorDistance = 4.5f, minimunCameraHeight = 0f, movementSpeed, maxMovementSpeed = 5f, moveDirection, 
        jumpVelocity = 5f, jumpCancelAcel = 5f, flipOutSpeed = 260f, ragdollTimer = 0;
    private bool isMoving, isGrounded, gravityAffected, jumpCancelled, isCrouching, isDead = false, cameraLockStatus = true, 
        cameraLockSetting, isStumbled = false;
    private int flipOutRevs=0, flipOutDirection;
    private Vector2 moveVector;



    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        
        gravityAffected = true;
        
        rigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        moveVector = new Vector2(0f,0f);
        

        cameraLockSetting = true;
        cameraLockStatus = true;

        flipHitBox.SetActive(false);

    }

    void Awake()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        StumbleCheck();

        if(isGrounded && this.GetPlayerTransform().rotation != Quaternion.identity && !isCrouching)
        {
            this.transform.rotation = Quaternion.identity;
        }

        if(!isStumbled) //Change this to check for a stumb
        {
            rigidbody.velocity = new Vector2(maxMovementSpeed * moveVector.x, rigidbody.velocity.y);
        }
        

        if(!isGrounded && !isStumbled)
        {
            FlipOut(flipOutDirection);
        }
        

        //changes the Camera lock and against the status
        if (cameraLockSetting == cameraLockStatus)
        {
            if (cameraLockStatus)
            {
                MoveCamera(cameraLockStatus);
            }
            else
            {
                MoveCamera(cameraLockStatus);
            }
        }
        else
        {
            if(cameraLockSetting)
            {
                GroundCheck();
                if (isGrounded)
                {
                    
                    cameraLockStatus = true;
                    MoveCamera(cameraLockStatus);
                }
                else
                {
                    MoveCamera(cameraLockStatus);
                }
            }
            else
            {
                
                cameraLockStatus = false;
                MoveCamera(cameraLockStatus);
            }
        }
        
        //isGrounded = Physics.CheckSphere(groundCheck.transform.position,.2f,lay)

        if(rigidbody.velocity.y < 0.75f*jumpVelocity || jumpCancelled)
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * Time.fixedDeltaTime * jumpCancelAcel;
        }

        if (!isCrouching)
        {
            moveDirection = moveVector.x;
        }
        

        if(isDead)
        {
            ragdollTimer += Time.deltaTime;
            if(ragdollTimer>1 && ragdollTimer<2)
            {
                SceneManager.LoadScene("Gameover", LoadSceneMode.Additive);
                ragdollTimer += 1;
            }
        }
    }


    public void MovementPerformed(InputAction.CallbackContext context)
    {
        if (isDead)
        {
            moveVector = new Vector2(0f, 0f);
            return;
        }
        moveVector = new Vector2(context.ReadValue<float>(), moveVector.y);

    

    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(isDead) return;
        if(context.started)
        {
            GroundCheck();
            
            
        }
        if(context.performed && isGrounded)
        {
            //crouch, slide, and then walk at a crouched movement speed

            //quarternions are required for rotations
            
            isCrouching = true;
            if(moveDirection != 0)
            {
                this.transform.rotation *= Quaternion.AngleAxis(moveDirection * 90f, Vector3.forward);
                
            }
            else
            {
                this.transform.rotation *= Quaternion.AngleAxis(90f, Vector3.forward);
                
            }

            
        }

        if(context.canceled&&isCrouching)
        {
            //stand up
            //this.transform.rotation = new Vector3(0, 0, 0);
            //quarternions is required to return the rotation
            
            if (moveDirection != 0)
            {
                this.transform.rotation *= Quaternion.AngleAxis(-1*moveDirection * 90f, Vector3.forward);
                
            }
            else
            {
                this.transform.rotation *= Quaternion.AngleAxis(-90f, Vector3.forward);
                
            }
            isCrouching = false;
        }
    }

    public void StumbleCheck()
    {
        if(stumbleBox.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //make that bitch stumble
            Debug.Log("Stumbled");
        }
    }

    public void GroundCheck()
    {
       
        
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
        if(isDead) return;
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

    //used for flipping out
    public void FlipLeft(InputAction.CallbackContext context)
    {
        if (isDead || isCrouching) return;
        if (context.started)
        {
            FlipOutHitbox.instance.ResetBox();
            flipHitBox.SetActive(true);

        }
        if(context.performed)
        {
            flipOutDirection = -1;
        }
        if(context.canceled)
        {
           flipHitBox.SetActive(false);
           flipOutDirection = 0;
        }
    }

    //used for flipping out
    public void FlipRight(InputAction.CallbackContext context)
    {
        if (isDead || isCrouching) return;
        if (context.started)
        {
            FlipOutHitbox.instance.ResetBox();
            flipHitBox.SetActive(true);

        }
        if (context.performed)
        {
            flipOutDirection = 1;
        }
        if (context.canceled)
        {
            flipHitBox.SetActive(false);
            flipOutDirection = 0;
        }
    }

    public void KillPlayer()
    {
        isDead = true;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
    }

    public bool GetPlayerDeathState()
    {
        return isDead;
    }
    //Getters and Setters

    public Vector3 GetPlayerPosition()
    {
        return this.transform.position;
    }

    public float GetPlayerSpeed()
    {
        return maxMovementSpeed;
    }

    public void SetCameraLock(bool setting)
    {
        cameraLockSetting = setting;
    }

    public void SetMinimumCameraHeight(float floorHeight)
    {
        minimunCameraHeight = floorHeight+CameraFloorDistance;
    }

    private void MoveCamera(bool isLocked)
    {
        Vector2 cameraHeight = new Vector2(this.transform.position.x, 0);
        
        if (isLocked)
        {
            cameraHeight.y = MainCam.transform.position.y;
        }
        else
        {
            cameraHeight.y = this.transform.position.y;
        }

        if (cameraHeight.y < minimunCameraHeight)
        {
            cameraHeight.y = minimunCameraHeight;
        }

        MainCam.transform.position = new Vector3(cameraHeight.x, cameraHeight.y, MainCam.transform.position.z);
    }

    public void FlipOut(float direction)
    {
        Quaternion q = Quaternion.AngleAxis(flipOutSpeed*direction, Vector3.forward);
        this.transform.rotation *= q;
    }

    public Transform GetPlayerTransform()
    {
        return this.transform;
    }


}
