using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_FermentedJacket : ItemParent
{
    public const float baselineTimer = 15f, quantityMultiplayer = 3f;
    private Color healthPipColor = new Color(1f,0.6f, 0.05f, 0.6f);
    

    private float timeCounter = 0f;
    public override void AddEvent()
    {
        ItemManager.PlayerKillsEnemy += ItemEventHandler;
    }

    public override void ItemFunctionality()
    {
        //Start the timer of a the Iso Shield For the health
        //1 shot shield that breaks after use

        ItemManager.instance.SetFermentedJacket(GetBeerTimer());
        PlayerController.instance.SetBeerShield(true);
        healthManager.instance.UpdateHealthDisplayColor(healthPipColor);
        Debug.Log("Check for functionality");
        

    }

    public float GetBeerTimer()
    {
        timeCounter = quantity > 1 ? baselineTimer + quantity * quantityMultiplayer : baselineTimer;
        return timeCounter;
    }

    
}
