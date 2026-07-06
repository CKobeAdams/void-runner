using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_JockStrap : ItemParent
{
    //the baseline for when you have 1 vs multiples
    const float baseline = 1f, multiplier = 0.2f;

    
    public override void AddEvent()
    {
        ItemManager.PlayerKillsEnemy += ItemEventHandler;
    }

    public override void ItemFunctionality()
    {
        float addedBoost = quantity > 1 ? baseline+quantity*multiplier : baseline;

        ItemManager.instance.AddTempBoost(addedBoost);
    }
}
