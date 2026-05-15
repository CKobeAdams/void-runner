using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance {get; private set; }

    [SerializeField]
    private GameObject basePlatform, trickBox;

    

    public List<GameObject> roomList, specialRooms, normalRooms;

    [SerializeField]
    private GameObject baseRoom, startRoom, ascendingRoom, descendingRoom, risingRoom, randomRoom, oneHopRoom, doubleHopRoom,
        oneSlideJumpRoom, doubleSlideJumpRoom, oneJumpRoom, dropFloorRoom;

    [SerializeField]
    private GameObject currentRoom, oldRoom;

    //private Dictionary((string, string), delegate) Trick

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        roomList = new List<GameObject>();
        roomList.Add(startRoom);

        currentRoom = startRoom;

        specialRooms = new List<GameObject>();
        specialRooms.Add(ascendingRoom);
        specialRooms.Add(descendingRoom);
        specialRooms.Add(risingRoom);
        //specialRooms.Add(UnityEngine.RandomRoom);


        normalRooms = new List<GameObject>();
        normalRooms.Add(baseRoom);
        normalRooms.Add(oneHopRoom);
        normalRooms.Add(doubleHopRoom);
        normalRooms.Add(oneSlideJumpRoom);
        normalRooms.Add(doubleSlideJumpRoom);
        normalRooms.Add(oneJumpRoom);
        normalRooms.Add(dropFloorRoom);

    }

    void Start()
    {
        for(int i=0; i<2; i++)
        {
            GenerateRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentRoom();
        Vector3 floorPos = currentRoom.transform.GetChild(1).position;
        CameraManager.instance.SetMinimumCameraHeight(floorPos.y);
        DeathFloor.instance.SetFloorPosition(floorPos.y);
    }

    public void GenerateRoom()
    {
        //GameObject newRoom = Instantiate(baseRoom, GetListEndNode().transform);
        //makes the new room and gets starting node

        //Change to UnityEngine.Random.Value
        float roomChooser = UnityEngine.Random.value;
        GameObject newRoom;

        //Reset back to 0.1f after testing
        if(roomChooser < 0.5f)
        {
            //UnityEngine.Random Special rooms
            newRoom = Instantiate(GenerateSpecialRoom());
        }
        else
        {
            newRoom = Instantiate(GenerateNormalRoom());
        }
        

        
        
        Transform newRoomStartNode = newRoom.transform.GetChild(0);

        //Gets the offset to the center of the room to the starting node
        Vector3 newRoomOffset =  newRoomStartNode.position - newRoom.transform.position;


        //places the starting node in the correct position
        Vector3 nodePosition = GetListEndNodePosition();
       
        newRoom.transform.position = new Vector3(nodePosition.x-newRoomOffset.x, nodePosition.y-newRoomOffset.y, nodePosition.z);

        string newRoomName = newRoom.GetComponent<ParentRoom>().name;
        string oldRoomName;
        if (roomList.Count>2)
        {
             oldRoomName = roomList[roomList.Count - 1].GetComponent<ParentRoom>().name;
        }
        else
        {
            oldRoomName = "Self";
        }
        

        //adds the starting room to a new room
        roomChooser = UnityEngine.Random.value;
        if(roomChooser<1f)
        {
            roomChooser = UnityEngine.Random.value;
            if(roomChooser<0.5f)
            {
                GenerateTrickBoxes((newRoomName, oldRoomName), newRoom);
            }
            else
            {
                GenerateTrickBoxes((newRoomName, "Self"), newRoom);
            }
        }
        
        roomList.Add(newRoom);
    }

    private Vector3 GetListEndNodePosition()
    {
        Transform endNode;
        GameObject endRoom;

        endRoom = roomList.Last();

        
        endNode = endRoom.transform.GetChild(2);
        

        return endNode.position;
    }

    public void DestroyRooms()
    {
        if(roomList.Count > 5)
        {
            GameObject targetRoom = roomList[0];
            roomList.RemoveAt(0);
            Destroy(targetRoom);
        }
    }

    private void CheckCurrentRoom()
    {

        Vector3 playerPos = PlayerController.instance.GetPlayerPosition();
        foreach(GameObject room in roomList)
        {
            Vector3 roomPos = room.transform.position;
            float roomHalfLength = room.transform.position.x - room.transform.GetChild(2).position.x;
            

            if(roomPos.x - roomHalfLength <= playerPos.x || roomPos.x + roomHalfLength <=playerPos.x)
            {
                currentRoom = room;
                /*try
                {
                    Debug.Log(currentRoom.GetComponent<ParentRoom>().name);
                }
                catch { }*/
                
            }

            
        }
    }

    //This method selects a room UnityEngine.Randomly from the special room list
    private GameObject GenerateSpecialRoom()
    {
        
        //return specialRooms[(int)Mathf.Floor(UnityEngine.Random.Range(0,specialRooms.Count))];
        //Line below should be commented and the line above should be uncommented after testing
        return specialRooms[0];

        
    }


    //selects a room UnityEngine.Randomly from the normal room list
    private GameObject GenerateNormalRoom()
    {
        
        //return normalRooms[(int)Mathf.Floor(UnityEngine.Random.Range(0, normalRooms.Count))];
        return normalRooms[0];
    }
   
    
    //Us the general manager to determine when to spawn another room
    //When a room has been spawned generate obstacles that the player needs to jump/crouch over

    private void GenerateTrickBoxes((string currentRoom, string prevRoom) index, GameObject room)
    {
       
        (Vector3 location, Func<Vector3, Vector3> piecewise) dictionaryCite;
        try
        {
            dictionaryCite = TrickDictionary.instance.CiteValue(index);

            
        }
        catch
        {
            return;
        }
        
        GameObject trick = Instantiate(trickBox);

        //trick.transform.position = dictionaryCite.location;
        //trick.GetComponent<GroundTrickBoxTrigger>.piecewiseTrick = dictionaryCite.piecewise;

        trick.transform.SetParent(room.transform,false);
        //trick.transform.localPosition += dictionaryCite.location;
        trick.transform.localPosition = new Vector3(0f,0f,0f);
        trick.transform.localPosition += dictionaryCite.location;
        

        trick.GetComponent<GroundTrickBoxTrigger>().piecewiseTrick = dictionaryCite.piecewise;

        Debug.Log("Index Found at: "+ index+"\nGenerating at:"+trick.transform.position);


    }
}
