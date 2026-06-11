using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortalScript : MonoBehaviour
{
    private const float endScrollTimer = 2f;
    
    private bool activated = false, startTimer = false;

    private float timer;

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
                SceneManager.LoadScene("Sub Run Screen", LoadSceneMode.Single);
            }
        }

    }
}
