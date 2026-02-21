using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrickBoxTrigger : MonoBehaviour
{
    private bool active = false, used = false;
    

    //the trigger will be active when the player is in the box
    //There are then 2 options
        //1. The player does the trick, then the hitbox is turned off and inactive afterwards
        //2. The player doesn't trick and then player class needs to be told it cant.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!used)
        {
            if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                active = true;
                Debug.Log("Trick Box is Active");
                PlayerController.instance.SetIsTrickable(active);

            }
            else
            {
                active = false;
            }


        }

        if(PlayerController.instance.GetHasTricked())
        {
            used = true;
        }
        
    }
}
