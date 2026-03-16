using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingBackground : MonoBehaviour
{
    private float foregroundVelocityRatio, midgroundVelocityRatio, backgroundVelocityRatio, 
        sizingRatio;

    private Vector2 playerVelocity, spriteBounds, pivotDisplacement;
    private Vector3 playerPosition, cameraPosition;

    //The sizing ratio is only based on the width to avoid stretching,
    //the ratio sould be a little bigger than the screen for the ease of transitions

    //The background images only have 1 size, currently 640 by 360
    private const float backgroundPixelWidth = 640f, backgroundPixelHeight = 320f,
        quickEaseMultiplier = 1.1f;

    [SerializeField]
    private GameObject foregroundSprite, midgroundSprite, backgroundSprite, backgroundParent;

    private GameObject spareForeground, spareMidground, spareBackground;
    // Start is called before the first frame update
    void Start()
    {
        foregroundVelocityRatio = 0.8f;
        midgroundVelocityRatio = 0.4f;
        backgroundVelocityRatio = 0.1f;


        pivotDisplacement = CameraManager.instance.GetCameraBaseDisplacement();
        sizingRatio = CameraManager.instance.GetCameraPixelHeight()/backgroundPixelHeight * quickEaseMultiplier * (32f/CameraManager.instance.GetCameraSize());

        backgroundSprite.transform.localScale = sizingRatio * backgroundSprite.transform.localScale;
        midgroundSprite.transform.localScale *= sizingRatio;
        foregroundSprite.transform.localScale *= sizingRatio;

        spriteBounds = new Vector2(backgroundSprite.GetComponent<SpriteRenderer>().bounds.extents.x,backgroundSprite.GetComponent<SpriteRenderer>().bounds.extents.y);

        spareBackground = Instantiate(backgroundSprite, backgroundParent.transform);
        spareBackground.transform.position = new Vector3(spareBackground.transform.position.x + spriteBounds.x*2, 
            spareBackground.transform.position.y, spareBackground.transform.position.z);

        spareMidground = Instantiate(midgroundSprite, backgroundParent.transform);
        spareMidground.transform.position = new Vector3(spareMidground.transform.position.x + spriteBounds.x * 2,
            spareMidground.transform.position.y, spareMidground.transform.position.z);

        spareForeground = Instantiate(foregroundSprite, backgroundParent.transform);
        spareForeground.transform.position = new Vector3(spareForeground.transform.position.x + spriteBounds.x * 2,
            spareForeground.transform.position.y, spareForeground.transform.position.z);

        //spriteBounds = spriteBounds.x * 2;
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = PlayerController.instance.GetPlayerVelocity();
        playerPosition = PlayerController.instance.GetPlayerPosition();
        backgroundParent.transform.position = new Vector3(playerPosition.x + pivotDisplacement.x, backgroundParent.transform.position.y, backgroundParent.transform.position.z);

        foreach(Transform child in backgroundParent.transform)
        {
            //check object tag for how fast the object should be moving

            

            switch (child.tag)
            {
                case "foreground":
                    //move the object
                    child.position = new Vector3(-playerVelocity.x * Time.deltaTime * foregroundVelocityRatio + child.position.x, child.position.y, child.position.z);
                    break;
                case "midground":
                    child.position = new Vector3(-playerVelocity.x * Time.deltaTime * midgroundVelocityRatio + child.position.x, child.position.y, child.position.z);
                    break;
                case "background":
                    child.position = new Vector3(-playerVelocity.x * Time.deltaTime * backgroundVelocityRatio + child.position.x, child.position.y, child.position.z);
                    break;
            }

            Debug.Log(child.localPosition.x);

            if (Mathf.Abs(child.localPosition.x) > spriteBounds.x * 2)
            {
                child.position = new Vector3(child.position.x - child.localPosition.x / Mathf.Abs(child.localPosition.x) * (spriteBounds.x * 4), child.position.y, child.position.z);
                Debug.Log(spriteBounds.x*2+ " pos: " + child.position.x);
            }





            //check if the object if offscreen
            //if it is 
        }
    }
}
