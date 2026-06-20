using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_HeartRefill : ItemParent
{
    [SerializeField]
    private RunDataSO runDataValues;

    private int timesAdded;

    /*
    name = "Heart Refill";
    description = "Top off your hearts to the MAX";
    price = 30;
    */

    // Start is called before the first frame update
    void Start()
    {
        
        ItemManager.instance.AddedToInventory += ItemEventHandler;
        //ItemManager.instance.AddedToInventory.AddListener(ItemFunctionality);
    }

    public override void ItemFunctionality()
    {
        if(timesAdded<quantity)
        {
            runDataValues.playerHealth = runDataValues.playerMaxHealth;
            timesAdded++;
        }
        
    }
    
}
