using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomFloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject basePlatform;
    private int platformCount = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //initialize 10 random platforms throughout the room
        for(int i=0; i<platformCount; i++)
        {
            GameObject newPlatform = Instantiate(basePlatform, this.transform, false);
            Vector3 platformPlacement = new Vector3(Random.value*60 -30,Random.value*13-3,0);
            if(platformPlacement.x > -2f && platformPlacement.x < 2f)
            {
                Debug.Log("displacing");
                platformPlacement.x = -3f;
            }

            newPlatform.transform.position += platformPlacement;
        }

    }


}
