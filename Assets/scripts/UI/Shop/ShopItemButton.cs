using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RunDataSO runDataValues;

    public ItemParent item;
    private Button button;
    private TMP_Text nameField;
    private bool HasPurchased = false;

    
    

    void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
        nameField = button.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        
    }

    public void DisplayItem()
    {
       
        button.gameObject.GetComponent<Image>().sprite = item.itemSprite;
        nameField.text = item.price+"T: "+item.name;

        
    }

    public void AttemptItemPurchase()
    {
        if(item.price <= runDataValues.threadCount && !HasPurchased)
        {
            ItemManager.instance.AddToPlayerInventory(item);
            HasPurchased = true;
            button.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
            nameField.text = "SOLD!";

            runDataValues.threadCount -= item.price;
            ShopManager.instance.UpdateThreadDisplay();
            
        }

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
        ShopManager.instance.SetDescriptionBox(item.description);
        if(item.price> runDataValues.threadCount)
        {
            button.gameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.5f);
        }
        else if(!HasPurchased)
        {
            button.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
       
        ShopManager.instance.ResetDescriptionBox();
        if(!HasPurchased)
        {
            button.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        
    }

}
