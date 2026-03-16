using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerHeightReset : MonoBehaviour
{
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    void awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!activated)
        {
            if (this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                activated = true;

                CameraManager.instance.SetCameraRaiseLower(0f);
            }
        }
    }
}
