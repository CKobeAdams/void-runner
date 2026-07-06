using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_EMG : ItemParent
{
    const float baseline = 0.5f, multiplier = 0.1f;

    public override void AddEvent()
    {
        ItemManager.FlipOut360 += ItemEventHandler;
    }

    public override void ItemFunctionality()
    {
        Debug.Log("FlipOut Performed! We are gaming");

        float addedBoost = quantity > 1 ? baseline + quantity * multiplier : baseline;

        ItemManager.instance.AddTempBoost(addedBoost);
    }
}
