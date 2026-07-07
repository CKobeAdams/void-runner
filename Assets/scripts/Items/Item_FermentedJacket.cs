using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_FermentedJacket : ItemParent
{
    const float baselineTimer = 15f, quantityMultiplayer = 1f;

    public override void AddEvent()
    {
        ItemManager.PlayerKillsEnemy += ItemEventHandler;
    }

    public override void ItemFunctionality()
    {
        //Start the timer of a the Iso Shield For the health
        //1 shot shield that breaks after use
    }
}
