using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BaseCube;

    // Start is called before the first frame update
    void Start()
    {
        //on start instantiate 1 to 7 retangles
        //randomize the length, hieght and position of them
        //attempt to use noise to give the feeling of randomness without shafting to the player
        //
    }

    // Update is called once per frame
    void Update()
    {
        //try to rewrite all of the platform cubes to stop stucking on the sides of the player
    }
    
    //Us the general manager to determine when to spawn another room
    //When a room has been spawned generate obstacles that the player needs to jump/crouch over
}
