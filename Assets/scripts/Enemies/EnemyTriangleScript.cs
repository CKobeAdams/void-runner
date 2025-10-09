using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriangleScript : EnemyParent
{
    //STATE MACHINE TIME
    private enum State
    {
        idle,
        seeking,
        attacking,

    }
    private State state;
    private float seekingDistance, attackingDistance, idleCounter = 0f;
    private float attackingCounter;
    private Vector3 diveAngle;

    [SerializeField]
    private const float diveSpeed = 15f , diveTime = 0.25f, diveCoolDown = 0.5f, diveStartUp = 0.25f, idleDirectionTimer = 0.5f, rotationSpeed = 40, rotationModifier = 90;
    private bool isDiving, onCoolDown, isLockedOn;
    private int idleDirection;
    private const int attackingDamage = 1, scoreValue = 300;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        state = State.idle;
        seekingDistance = 12.5f;
        attackingDistance = 6f;
        idleDirection = 1;
        isDiving = false;
        health = 1f;
        isDead = false;
        movementSpeed = 4f;
        
        rigidBody = GetComponent<Rigidbody2D>();

        
            
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            EnemyManager.instance.RemoveEnemy(this.GetComponent<EnemyParent>());
        }


        //if else statements to determine the distance
        // switch state based on distance
        float distanceToPlayer = Utilities.instance.DistanceFormula(this.transform.position, PlayerController.instance.GetPlayerPosition());
        if (!isDiving)
        {

            if (distanceToPlayer < attackingDistance)
            {
                rigidBody.velocity = Vector3.zero;
                state = State.attacking;
            }
            else if(distanceToPlayer < seekingDistance)
            {
                transform.rotation = Quaternion.identity;
                rigidBody.velocity = Vector3.zero;
                state = State.seeking;
            }
            else
            {
                transform.rotation = Quaternion.identity;
                rigidBody.velocity = new Vector3(movementSpeed * idleDirection, 0f, 0f);
                state = State.idle;
            }
            //use a switch statement to run different state methods
            //idle
            //seeking
            //attacking

        
            
        }

        switch (state)
        {
            case State.idle:
                Idle();
                break;
            case State.seeking:
                Seeking();
                break;
            case State.attacking:
                Attacking();
                break;
        }

        if (!PlayerController.instance.GetPlayerDeathState())
        {
            if (this.GetComponent<PolygonCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                PlayerController.instance.TakeDamage(attackingDamage);
                TakeDamage(health, false);
            }
        }

        
        
    }

    private void Idle()
    {
        
        if(idleCounter>=idleDirectionTimer)
        {
            idleDirection *= -1;
            idleCounter = 0;
            rigidBody.velocity = new Vector3(movementSpeed * idleDirection, 0f, 0f);
        }

        idleCounter += Time.deltaTime;

    }

    private void Seeking()
    {
        //make use of the physics system
        //Use Velocitys

        //Vector3 playerPosition = PlayerController.instance.GetPlayerPosition();
        //Vector3 velocity;
        //float step = Time.deltaTime * movementSpeed;
        //transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);

        Vector3 playerPosition = PlayerController.instance.GetPlayerPosition();
        Vector3 vectorToPlayer = Utilities.instance.SlopeFormula(playerPosition, transform.position).normalized;
        rigidBody.velocity = new Vector3(vectorToPlayer.x * movementSpeed, vectorToPlayer.y * movementSpeed, vectorToPlayer.z * movementSpeed);

        
    }

    private void Attacking()
    {
        Vector3 playerPosition = PlayerController.instance.GetPlayerPosition();
        float totalTime = diveStartUp + diveTime + diveCoolDown;
        float diveTotalTime = diveStartUp + diveTime;

      

        if (attackingCounter >= totalTime)
        {
            isDiving = false;
            onCoolDown = false;
            isLockedOn = false;
            attackingCounter = 0;
            return;
        }
        else if (attackingCounter >= diveTotalTime)
        {
            onCoolDown = true;
        }
        else if(attackingCounter >= diveStartUp)
        {
            isDiving = true;
        }
        
        

        if (!isDiving && !onCoolDown)
        {
            //tried using quaternions to rotate it
            //transform.rotation = Quaternion.LookRotation(new Vector3(playerPosition.y,playerPosition.x,0), new Vector3(0f,0f,1f));

            //lets try using LookRotate
            //transform.LookAt(new Vector3(), Vector3.up);

            //time to use the real math
            //float playerAngle = Vector3.Angle(playerPosition,  transform.position);

            //transform.rotation *= new Quaternion(0f, 0f, playerAngle, 0f);

            //Looked it up so here we go
            //during start up the triangle will angle it's point towards player


            Vector3 delta = playerPosition - transform.position;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);



        }
        else if(!isLockedOn)
        {
            //dive with the physics system
            diveAngle = Utilities.instance.SlopeFormula(playerPosition, transform.position).normalized;
            rigidBody.velocity = diveAngle*diveSpeed;

            isLockedOn = true;
        }


        attackingCounter += Time.deltaTime;
    
        


       
    }
    //movement Function

    //private void MovementFunction
    //Attack Function

    public override void TakeDamage(float damageTaken, bool adjustScore)
    {
        health -= damageTaken;
        
        if(health<=0)
        {
            isDead = true;
            EnemyManager.instance.RemoveEnemy(this.GetComponent<EnemyParent>());
            if(adjustScore)
            {
                UIManager.instance.AdjustScore(scoreValue);
            }
            
            
        }

       
       
    }
}
