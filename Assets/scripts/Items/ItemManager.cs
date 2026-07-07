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
    //public Dictionary<string, float> temporaryStatBonuses = new Dictionary<string, float>();

    [SerializeField]
    private float itemTempBoostSpeed = 0, boostSpeedDecayTime = 1.5f/*units a second*/, 
        boostDecayStartTimer = 0.2f, boostTimeCounter = 0f;

    private bool isBoosted = false;


    public delegate void EventHandler();

    //public static event EventHandler SpeedBoostAdded;

    //public UnityEvent AddedToInventory;
    //Add all events that could happen to the player
    public static event EventHandler AddedToInventory;
    public static event EventHandler PlayerKillsEnemy;
    public static event EventHandler FlipOut360;

    [SerializeField]
    private ItemParent item_HeartRefill, item_JockStrap, item_FermentedJacket, item_EMG;





    void Awake()
    {
        instance = this;

        //AddedToInventory = new UnityEvent();


        masterEventList.Add(AddedToInventory);
        masterEventList.Add(PlayerKillsEnemy);
        masterEventList.Add(FlipOut360);

        masterItemList.Add(item_HeartRefill);
        masterItemList.Add(item_JockStrap);
        masterItemList.Add(item_EMG);
        //masterItemList.Add(item_EMG);
        //masterItemList.Add(item_FermentedJacket);

        
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
        if(isBoosted)
        {
            if(boostTimeCounter > boostDecayStartTimer)
            {
                float sinceDecayStart = boostTimeCounter - boostDecayStartTimer;
                itemTempBoostSpeed = itemTempBoostSpeed * (1-sinceDecayStart / boostSpeedDecayTime);
            }

            if(itemTempBoostSpeed<0)
            {
                isBoosted = false;
                itemTempBoostSpeed = 0;
                boostTimeCounter = 0;
            }



            boostTimeCounter += Time.deltaTime;
            PlayerController.instance.AdjustBoostSpeed(itemTempBoostSpeed);

        }

        Debug.Log(itemTempBoostSpeed);
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

    public void AddTempBoost(float boostAdded)
    {
        itemTempBoostSpeed += boostAdded;
        isBoosted = true;
        ResetDecayTimer();
    }

    public void ResetDecayTimer()
    {

    }

    public void InvokeEvent_PlayerKillsEnemy()
    {
        PlayerKillsEnemy?.Invoke();
    }

    public void InvokeEvent_FlipOut360()
    {
        FlipOut360?.Invoke();
    }
}

