using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; private set; }

    [SerializeField]
    private Button[] shopItemButtons = new Button[3];
    private ItemParent[] shopList = new ItemParent[4];
    private string defaultDescriptBoxText = "Hover over an item to read its description";
    [SerializeField]
    private TMP_Text descriptionBox;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PullFromMasterList();
        DisplayShopList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PullFromMasterList()
    {
        for(int i=0; i<shopList.Length; i++)
        {
            shopList[i] = ItemManager.instance.RandomFromList();
        }
    }
    
    public void DisplayShopList()
    {
        for(int i = 0;i<shopItemButtons.Length; i++)
        {
            shopItemButtons[i].GetComponent<ShopItemButton>().item = shopList[i];
            shopItemButtons[i].GetComponent<ShopItemButton>().DisplayItem();
        }
    }

    public void SetDescriptionBox(string text)
    {
        descriptionBox.text = text;
    }

    public void ResetDescriptionBox()
    {
        descriptionBox.text = defaultDescriptBoxText;
    }
}
