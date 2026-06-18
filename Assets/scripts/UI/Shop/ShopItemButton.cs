using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemButton : MonoBehaviour
{
    public ItemParent item;
    private Button button;
    

    void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    public void DisplayItem()
    {
        this.gameObject.GetComponent<Image>().sprite = item.itemSprite;

        
    }

    public void WhenClicked()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        ShopManager.instance.SetDescriptionBox(item.description);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ShopManager.instance.ResetDescriptionBox();
    }

}
