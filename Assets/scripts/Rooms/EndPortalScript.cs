using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortalScript : MonoBehaviour
{
    private const float endScrollTimer = 2f;
    
    private bool activated = false, startTimer = false;

    private float timer;
    private const string subRun = "Sub Run Screen", shopScreen = "ShopScreen";
    private int shopLevelSpacer = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!activated)
        {
            if(this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                //lock the player's x directional movement start the clock of transitioning to the next scene
                startTimer = true;
                activated = true;
                PlayerController.instance.LockForEndPortal();
            }
        }

        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer>endScrollTimer)
            {
                UIManager.instance.PauseTimer();
                UIManager.instance.PassValuesToRunData();
                if (RoomManager.instance.GetLevelCounter() % shopLevelSpacer == 0)
                {
                    SceneManager.LoadScene(shopScreen, LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene(subRun, LoadSceneMode.Single);
                }
                    
            }
        }

    }
}
