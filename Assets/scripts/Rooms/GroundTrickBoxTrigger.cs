using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundTrickBoxTrigger : MonoBehaviour
{
    private bool active = false, used = false;
    
    public Func<Vector3, Vector3> piecewiseTrick
    {
        get;

        set;
    }


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
                PlayerController.instance.SetIsTrickable(active, piecewiseTrick, this.transform.position);

            }
            else
            {
                active = false;
                PlayerController.instance.SetIsTrickable(active);

            }


        }

        if(PlayerController.instance.GetHasTricked())
        {
            used = true;
            PlayerController.instance.SetIsTrickable(false);
        }
        
    }
}
