using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ItemParent : ScriptableObject
{
    [SerializeField]
    protected RunDataSO runDataValues;
    public string name;
    public Sprite itemSprite;
    public int price;
    public int quantity;
    public string description;


    //in the child,on start add the item function to the corresponding event listener
    public virtual void ItemFunctionality()
    {
        Debug.Log("Parent Virtual Call");
    }

    public virtual void AddEvent()
    {
        Debug.Log("Parent AddEvent Called");
    }

    protected void ItemEventHandler()
    {
        //Debug.Log("The Event Was Called");
        ItemFunctionality();
    }

    public virtual void ResetQuantity()
    {
        quantity = 0;
    }
}
