using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrickBoxTrigger : MonoBehaviour
{
    bool active = false;
    bool HasTricked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            active = true;
            Debug.Log("Trick Box is Active");
        }
    }
}
