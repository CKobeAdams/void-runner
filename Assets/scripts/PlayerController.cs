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
    private GameObject groundCheck, flipHitBox, wallCheck, wallHeadCheck, stumbleBox;

    [SerializeField]
    private Camera MainCam;

    [SerializeField]
    private float CameraFloorDistance = 4.5f, minimunCameraHeight = 0f, movementVelocity = 0, maxMovementSpeed = 20f, moveDirection,
        jumpVelocity = 6f, jumpCancelAcel = 5f, flipOutSpeed = 260f, ragdollTimer = 0, moveAccel = 10f, decceleration = -0.01f, 
        startUpSpeed = 2.5f, startUpAcceleration = 50f, turningAcceleration = 15f, crouchingDecceleration = 0, invincibleTimer = 2.5f, 
        invincibleCounter = 0f, airMoveAcceleration = 5f, airMoveVelocity = 0f, airMoveDifferentialCap = 2f;
    private bool isMoving, isGrounded, gravityAffected, jumpCancelled, isCrouching, isDead = false, cameraLockStatus = true, 
        cameraLockSetting, isStumbled = false, isWalled, isStuckOnWall, tookDamage;
    private int flipOutRevs = 0, flipOutDirection, unstumbleCount = 0, playerHealth = 3;
    private const int stumblePressNeeded = 3, playerMaxHealth = 3;
    private const float wallCheckOffset = 0.1f;
    private Vector2 moveVector;

    private enum runningState
    {
        idle,
        startUp,
        accelerating,
        topSpeed,
        turning,
        deccelerating
    }

    /*private enum airMoveState
    {
        moveRight,
        moveLeft,
        none,
    }

    private airMoveState airState;*/
    private runningState runState;

    // Start is called before the first frame update
    void Start()
    {
        runState = runningState.idle;
        

        
        
        gravityAffected = true;
        
        rigidbody = GetComponent<Rigidbody2D>();
        
        


    }

    void Awake()
    {
        instance = this;
        playerHealth = playerMaxHealth;
        isGrounded = true;
        moveVector = new Vector2(0f, 0f);


        cameraLockSetting = true;
        cameraLockStatus = true;

        flipHitBox.SetActive(false);
    }

    void Update()
    {
        Vector3 CheckerPos = GetPlayerTransform().position;
        CheckerPos.y += wallCheckOffset;


        wallCheck.transform.position = CheckerPos;

        WallCheck();
        GroundCheck();
        StumbleCheck();

        if (!isStumbled) //Change this to check for a stumb
        {
            //THIS IS WHERE WE ACTUALLY MOVE THE CHARACTER
            if (isStuckOnWall)
            {
                moveDirection = 0;
                ManageMovementInput();
            }
            else
            {
                ManageMovementInput();
            }


        }

        if (movementVelocity < 0)
        {
            moveDirection = -1;
        }
        else if (movementVelocity > 0)
        {
            moveDirection = 1;
        }
        else
        {
            moveDirection = 0;
        }

        if(tookDamage)
        {
            invincibleCounter += Time.deltaTime;
            


            if((invincibleCounter*1000)%2==0)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }

            if(invincibleCounter>=invincibleTimer)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                invincibleCounter = 0f;
                tookDamage = false;
                
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        

        if(isGrounded && this.GetPlayerTransform().rotation != Quaternion.identity && !isCrouching)
        {
            this.transform.rotation = Quaternion.identity;
        }

        

        /*if (!isStumbled) //Change this to check for a stumb
        {
            //THIS IS WHERE WE ACTUALLY MOVE THE CHARACTER
            if (!isCrouching && !isStuckOnWall)
            {
                ManageMovementInput();
            }
            else if (!isCrouching && isStuckOnWall)
            {
                moveDirection = 0;
                ManageMovementInput();
            }


        }*/
        

        if(!isGrounded && !isStumbled)
        {
            FlipOut(flipOutDirection);
        }
        

       
        
        //isGrounded = Physics.CheckSphere(groundCheck.transform.position,.2f,lay)

        if(rigidbody.velocity.y < 0.75f*jumpVelocity || jumpCancelled)
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * Time.fixedDeltaTime * jumpCancelAcel;
        }

        
        


        if (isDead)
        {
            ragdollTimer += Time.deltaTime;
            if(ragdollTimer>1 && ragdollTimer<2)
            {
                UIManager.instance.PauseTimer();
                UIManager.instance.PassValuesToRunData();
                SceneManager.LoadScene("Gameover", LoadSceneMode.Additive);
                ragdollTimer += 1;
            }
        }

        

        
    }

    private void ManageMovementInput()
    { //this is where we change acceleration
        //change the color of the object in the sprite renderer for the different stages
            //start up
            //accelerating
            //maxspeed
        //bouncing off of weird invisible "wall" when moving backwards

        if(!isGrounded)
        {
            //carry movement speed if its fast enough
            //jumping forward carries movement
            //resets when the player touches the ground again
            

            if(moveVector.x==1f)
            {
                airMoveVelocity += airMoveAcceleration * Time.deltaTime;
                /*if(Mathf.Abs(airMoveVelocity) > airMoveDifferentialCap)
                {
                    airMoveVelocity = airMoveDifferentialCap;
                }*/
                if(Mathf.Abs(airMoveVelocity) < airMoveDifferentialCap)
                {
                    
                    rigidbody.velocity = new Vector2(movementVelocity+ airMoveVelocity, rigidbody.velocity.y);
                }
                else
                {
                    airMoveVelocity = airMoveDifferentialCap;
                }
                
            }
            if(moveVector.x==-1f)
            {
                airMoveVelocity -= airMoveAcceleration * Time.deltaTime;
                if(Mathf.Abs(airMoveVelocity)<airMoveDifferentialCap)
                {
                    
                    rigidbody.velocity = new Vector2(movementVelocity + airMoveVelocity, rigidbody.velocity.y);
                }
                else
                {
                    airMoveVelocity = -airMoveDifferentialCap;
                }
                
            }

        }

        if(isGrounded||isCrouching)
        {
            /*if (moveDirection == 0)
            {
                movementVelocity = startUpSpeed * moveVector.x + moveAccel * Time.deltaTime * moveVector.x;
            }
            if (moveDirection != moveVector.x)
            {

                movementVelocity = movementVelocity + decceleration * Time.deltaTime * moveDirection;
                if (moveVector.x == 0 && Mathf.Abs(movementVelocity) < 0.1f)
                {
                    movementVelocity = 0;
                    moveDirection = 0;
                    return;
                }
            }
            else
            {
                movementVelocity = movementVelocity + moveAccel * Time.deltaTime * moveVector.x;
            }

            if (Mathf.Abs(movementVelocity) > maxMovementSpeed)
            {
                movementVelocity = maxMovementSpeed;
            }*/

            //Maybe try using move towards for the velocity

            //Debug.Log(movementVelocity);

            switch (runState)
            {
                //when the player is standing still
                case runningState.idle:
                    this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                    if (moveVector.x != 0 && !isCrouching)
                    {
                        moveDirection = moveVector.x;
                        runState = runningState.startUp;
                        movementVelocity = 0.001f *moveVector.x;
                    }
                    break;

                //rapid acceleration up to the speed, i.e. smooth movement from not moving to moving
                case runningState.startUp:
                    movementVelocity = movementVelocity + startUpAcceleration * Time.deltaTime * moveVector.x;
                    this.GetComponent<SpriteRenderer>().color = new Color(180f/255f, 66f/255f, 1f, 1f);
                    //0 within the this if statement 
                    if (moveVector.x != moveDirection && moveVector.x != 0)
                    {
                        runState = runningState.turning;
                    }
                    else if(Mathf.Abs(movementVelocity) >= startUpSpeed)
                    {
                        runState = runningState.accelerating;
                    }

                    break;

                //move but gaining speed to top speed
                case runningState.accelerating:
                    movementVelocity = movementVelocity + moveAccel * Time.deltaTime * moveDirection;
                    this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 1f);

                    if (Mathf.Abs(movementVelocity) >= maxMovementSpeed)
                    {
                        runState = runningState.topSpeed;
                        movementVelocity = maxMovementSpeed*moveVector.x;
                    }

                    if(moveVector.x != 0 && moveVector.x != moveDirection)
                    {
                        runState = runningState.turning;
                    }
                    else if(moveVector.x == 0)
                    {
                        runState = runningState.deccelerating;
                    }


                    break;

                //top speed, moving and can only slow down
                case runningState.topSpeed:
                    movementVelocity = maxMovementSpeed*moveDirection;
                    this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.0f, 0.5f, 1f);

                    if (moveVector.x != 0 && moveVector.x != moveDirection)
                    {
                        runState = runningState.turning;
                    }
                    else if(moveVector.x == 0)
                    {
                        runState = runningState.deccelerating;
                    }

                    break;

                //Moving but there is no player input
                case runningState.deccelerating:


                    
                    if(isCrouching)
                    {
                        movementVelocity = movementVelocity + crouchingDecceleration * Time.deltaTime * -moveDirection;

                    }
                    else
                    {
                        movementVelocity = movementVelocity + decceleration * Time.deltaTime * -moveDirection;
                    }
                    
                    //movementVelocity = movementVelocity + moveAccel * Time.deltaTime * moveDirection;
                    

                    
                    this.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0f, 1f);

                    //if(moveDirection == moveVector.x && moveVector.x == 0)
                    if(movementVelocity <= 1f && movementVelocity >= -1f)
                    {
                        movementVelocity = 0;
                        runState = runningState.idle;
                    }

                    if(!isCrouching)
                    {
                        if (moveVector.x == moveDirection && movementVelocity != 0)
                        {
                            runState = runningState.accelerating;
                        }
                        else if (moveVector.x != moveDirection && moveVector.x != 0)
                        {
                            runState = runningState.turning;
                        }
                    }

                    break;

                //moving but player input is the opposite direction of the movement
                case runningState.turning:
                    movementVelocity = movementVelocity + turningAcceleration * Time.deltaTime * (-moveDirection);
                    this.GetComponent<SpriteRenderer>().color = new Color(1f, 148f/255f, 66f/255f, 1f);

                    if (movementVelocity <= 0.1f && movementVelocity >= -0.1f)
                    {
                        runState = runningState.startUp;
                        movementVelocity = 0.1f*moveVector.x;
                        
                    }

                    if(moveVector.x == moveDirection)
                    {
                        runState = runningState.startUp;
                        movementVelocity = 0.1f * moveVector.x;
                    }
                    else if (moveVector.x == 0)
                    {
                        runState = runningState.deccelerating;
                    }


                    break;
            }


            rigidbody.velocity = new Vector2(movementVelocity, rigidbody.velocity.y);

            
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
            runState = runningState.deccelerating;
            
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
            if(!isWalled && !isCrouching)
            {
                if(!isStumbled)
                {
                    unstumbleCount = 0;
                }
                isStumbled = true;
                
            }
            
        }
        else
        {
            isStumbled = false;
        }
    }

    public void GroundCheck()
    {
       
        
        if(groundCheck.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            airMoveVelocity = 0;
            isGrounded = true; 
        }
        else
        {
            isGrounded = false;
        }

    }

    public void WallCheck()
    {
        if(wallCheck.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isWalled = true;
        }
        else
        {
            isWalled = false;
        }

        if (!isCrouching && !isGrounded && isWalled && stumbleBox.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            isStuckOnWall = true;
        }
        else
        {
            isStuckOnWall = false;
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
            if(isStumbled)
            {
                unstumbleCount++;
                if(unstumbleCount>=stumblePressNeeded)
                {
                    this.transform.rotation = Quaternion.identity;
                    isStumbled = false;
                    
                }
                
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

    public void TakeDamage(int damage)
    {
        if(!tookDamage)
        {
            playerHealth = playerHealth - damage;
            healthManager.instance.UpdateHealthDisplay(playerHealth);
            tookDamage = true;
           
           

        }
        

       
        //touch the canvas

        if(playerHealth <= 0)
        {
            isDead = true;
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

    public void FlipOut(float direction)
    {
        Quaternion q = Quaternion.AngleAxis(flipOutSpeed*direction, Vector3.forward);
        this.transform.rotation *= q;
    }

    public Transform GetPlayerTransform()
    {
        return this.transform;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public Vector2 GetMoveVector()
    {
        return moveVector;
    }

    public int GetCurrentHealth()
    {
        return playerHealth;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }

}
