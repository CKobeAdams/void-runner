using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemParent : ScriptableObject
{
    public string name;
    public Sprite itemSprite;
    public int price;
    public int quantity;
    public string description;


    //in the child,on start add the item function to the corresponding event listener
    public virtual void ItemFunctionality()
    {

    }

    protected void ItemEventHandler(object sender, EventArgs e)
    {
        ItemFunctionality();
    }
}
