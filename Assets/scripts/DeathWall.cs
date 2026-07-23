using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    public static DeathWall instance { get; private set; }

    //Change the wall to 1 constant speed to make it to the end of the level in 3 minutes
    [SerializeField]
    private float wallSpeed = 3f, speedMarker,
           wallDamage = 10000f;

    


    private bool playerKilled = false, onScreen;
    private Rigidbody2D rigidBody;
    private const int sourceCode = 1, averageRoomLength = 60;
    private int roomCap, levelCount, shopCount =0;
    private float desiredLevelTime = 240f, headStartTimer = 2f, headStartCounter = 0; //this is in seconds
    private float baseSpeed = 5f, speedLevelMultplier = 0.5f, speedCap = 100f, shopMultiplier = 3;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        wallSpeed = 1f;
        
    }

    void Start()
    {
        roomCap = RoomManager.instance.GetRoomCountCap();
        wallSpeed = (float)roomCap * (float)averageRoomLength / desiredLevelTime;
        headStartCounter = 0;
        levelCount = RoomManager.instance.GetLevelCounter();
        shopCount = ((int)levelCount - 1) / 3;

            wallSpeed = baseSpeed + levelCount * speedLevelMultplier + (Mathf.Pow(1.25f, shopMultiplier * shopCount - shopMultiplier));
    }

    // Update is called once per frame
    void Update()
    {
        //This gives players a head start on the wall of death
        if(headStartCounter<headStartTimer)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
            headStartCounter += Time.deltaTime;
        }
        else
        {
            rigidBody.velocity = new Vector2(wallSpeed, 0);
        }
            
    }

    void FixedUpdate()
    {
        if(!playerKilled)
        {

            //uses the util function for the disctance formula with the GetPlayerPosition
            /*if ( Utilities.instance.DistanceFormula(this.transform.position, PlayerController.instance.GetPlayerPosition()) >= 15f )
            {
                rigidBody.velocity = new Vector2(PlayerController.instance.GetPlayerSpeed(), 0f);
            }
            else
            {
                rigidBody.velocity = new Vector2(wallSpeed + Time.deltaTime * wallAcceleration, 0f);
            }*/
            

            /*if (Utilities.instance.DistanceFormula(this.transform.position, PlayerController.instance.GetPlayerPosition()) >= wallDistance
                && currentSpeed < PlayerController.instance.GetPlayerSpeed())
            {
                rigidBody.velocity = new Vector2(PlayerController.instance.GetPlayerSpeed(), 0f);
            }
            else
            {
                rigidBody.velocity = new Vector2(currentSpeed, 0f);
            }*/


            if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                playerKilled = true;
                PlayerController.instance.KillPlayer();
            }
            float playerY = PlayerController.instance.GetPlayerPosition().y;
            this.transform.position = new Vector3(this.transform.position.x, playerY, this.transform.position.z);
        }

        if(this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            EnemyManager.instance.EntityHurtSearch(this.GetComponent<BoxCollider2D>(), wallDamage, sourceCode);
        }
        
        
    }

    public void ResetWallTimerCount()
    {
        
    }
}
