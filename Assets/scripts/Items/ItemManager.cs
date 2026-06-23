using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance { get; private set; }

    [SerializeField]
    private RunDataSO runDataValues;

    public List<ItemParent> masterItemList = new List<ItemParent>();
    public List<EventHandler> masterEventList = new List<EventHandler>();
    public Dictionary<string, float> temporaryStatBonuses = new Dictionary<string, float>();

    public delegate void EventHandler();


    //public UnityEvent AddedToInventory;
    public static event EventHandler AddedToInventory;

    [SerializeField]
    private ItemParent item_HeartRefill;




    void Awake()
    {
        instance = this;

        //AddedToInventory = new UnityEvent();


        masterEventList.Add(AddedToInventory);

        masterItemList.Add(item_HeartRefill);

        
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(ItemParent item in runDataValues.inventory)
        {
            item.AddEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayerInventory(ItemParent item)
    {
        bool alreadyHasItem = false;
        /*int location
        for(int i=0; i<runDataValues.count; i++)
        {
            if (runDataValues[i].name === item.name)
            {
                alreadyHasItem = true;
            }
        }

        if(alreadyHasItem)
        {

        }
        else
        {

        }*/

        foreach(ItemParent i in runDataValues.inventory)
        {
            if(i.name == item.name)
            {
                alreadyHasItem = true;
                //i.quantity++;
            }
        }

        if(!alreadyHasItem)
        {
            item.quantity = 1;
            runDataValues.inventory.Add(item);
            item.AddEvent();
        }
        else
        {
            item.quantity++;
        }

        AddedToInventory?.Invoke();



    }

    public ItemParent RandomFromList()
    {
        float randomChooser = UnityEngine.Random.value;
        randomChooser = Mathf.Floor(randomChooser * masterItemList.Count);
        return masterItemList[(int)randomChooser];
    }

    public ItemParent SpecificFromList()
    {
        return masterItemList[1];
    }
}
