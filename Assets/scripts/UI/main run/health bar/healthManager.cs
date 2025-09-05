using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    public static healthManager instance { get; private set; }

    [SerializeField]
    private Sprite fullHeart, emptyHeart;

    [SerializeField]
    private Image healthPip;

    [SerializeField]
    private GameObject healthContainer;

    private List<Image> healthBarImages;
    private int playerMaxHealth, playerCurrentHealth;
    

    //Start is called before the first frame update
    void Start()
    {
        
        healthBarImages = new List<Image>();
        healthBarImages.Add(healthPip);
        playerMaxHealth = PlayerController.instance.GetMaxHealth();
        playerCurrentHealth = PlayerController.instance.GetCurrentHealth();

        SetHealthPips(PlayerController.instance.GetMaxHealth());
        
    }


    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //ChangeCurrentHealth((int)1);
    }

    public void UpdateHealthDisplay (int newCurrentHealth)
    {
        for(int i=playerMaxHealth-1; i>=newCurrentHealth; i--)
        {
            healthBarImages[i].sprite = emptyHeart;
        }
        playerCurrentHealth = newCurrentHealth;
    }

    public void ChangeMaxHealth(int newMaxHealth)
    {

    }

    public void SetHealthPips(int setHealth)
    {
        Image newImage;
        Vector3 pipPos = healthPip.transform.position;


        for(int i=1; i<setHealth; i++)
        {
            newImage = Instantiate(healthPip, this.transform);
            //change this magic number later
            newImage.transform.position = new Vector3(pipPos.x-i*36, pipPos.y, 0);
            healthBarImages.Add(newImage);
        }
    }
}
