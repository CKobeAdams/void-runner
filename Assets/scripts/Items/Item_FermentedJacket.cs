using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_FermentedJacket : ItemParent
{
    private const float baselineTimer = 15f, quantityMultiplayer = 1f;

    private bool isShielded = false;

    private float timeCounter = 0f;
    public override void AddEvent()
    {
        ItemManager.PlayerKillsEnemy += ItemEventHandler;
    }

    public override void ItemFunctionality()
    {
        //Start the timer of a the Iso Shield For the health
        //1 shot shield that breaks after use

        isShielded = true;
        timeCounter = 0f;


    }

    void Update()
    {
        Debug.Log("Update is called from the fermented JacketS");
        if(isShielded)
        {
            //change the color of the hearts to orange
            //add a timer
        }
    }
}
