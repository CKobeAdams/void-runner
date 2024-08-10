using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField]
    private float wallSpeed = 3f, wallAcceleration = 0.25f, currentSpeed = 0, speedMarker;

    private bool playerKilled = false, onScreen;
    private Rigidbody2D rigidBody;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        wallSpeed = 1f;
        wallAcceleration = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {

        currentSpeed = wallSpeed + Time.time* wallAcceleration;
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
            

            if (Utilities.instance.DistanceFormula(this.transform.position, PlayerController.instance.GetPlayerPosition()) >= 15f
                && currentSpeed < PlayerController.instance.GetPlayerSpeed())
            {
                rigidBody.velocity = new Vector2(PlayerController.instance.GetPlayerSpeed(), 0f);
            }
            else
            {
                rigidBody.velocity = new Vector2(currentSpeed, 0f);
            }


            if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                playerKilled = true;
                PlayerController.instance.KillPlayer();
            }
            float playerY = PlayerController.instance.GetPlayerPosition().y;
            this.transform.position = new Vector3(this.transform.position.x, playerY, this.transform.position.z);
        }
        
        
    }
}
