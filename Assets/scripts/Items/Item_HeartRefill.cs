using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Item_HeartRefill : ItemParent
{
    

    public int timesAdded;

    /*
    name = "Heart Refill";
    description = "Top off your hearts to the MAX";
    price = 30;
    */

    // Start is called before the first frame update

    public override void AddEvent()
    {
        
        ItemManager.AddedToInventory += ItemEventHandler;
        //Debug.Log("Event has been Added!");
       
        //ItemManager.instance.AddedToInventory.AddListener(ItemFunctionality);
    }

    public override void ItemFunctionality()
    {
        //Debug.Log("Child Override call");

        if(timesAdded<quantity)
        {
            //Debug.Log("WE SUPER FUNCTIONAL IN THIS BITCH");
            runDataValues.playerHealth = runDataValues.playerMaxHealth;
            timesAdded++;
        }
        
    }

    public override void ResetQuantity()
    {
        quantity = 0;
        timesAdded = 0;
    }
    
}
