using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemParent item;
    private Button button;
    private TMP_Text nameField;
    
    

    void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
        nameField = button.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
    }

    public void DisplayItem()
    {
        Debug.Log(button.gameObject.GetComponent<Image>().sprite);
        Debug.Log(item);
        button.gameObject.GetComponent<Image>().sprite = item.itemSprite;
        nameField.text = item.name;

        
    }

    public void WhenClicked()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("The Hover Was Called");
        ShopManager.instance.SetDescriptionBox(item.description);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("The Hover Was Called");
        ShopManager.instance.ResetDescriptionBox();
    }

}
