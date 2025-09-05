using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearScript : MonoBehaviour
{

    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!activated)
        {
            if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                activated = true;

                UIManager.instance.RoomCleared();
            }
        }
    }
}
