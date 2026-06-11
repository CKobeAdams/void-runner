using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RoomManager : MonoBehaviour
{
    //This File Manages rooms and manipulates the level counter of RunDataSO
    //The Manipulation of rundata happens near the ends of the Start() and Awake() functions

    public static RoomManager instance {get; private set; }

    [SerializeField]
    private GameObject basePlatform, trickBox;

    [SerializeField]
    private RunDataSO runDataValues;
    

    public List<GameObject> roomList, specialRooms, normalRooms;

    [SerializeField]
    private GameObject baseRoom, startRoom, endRoom, ascendingRoom, descendingRoom, risingRoom, oneHopRoom, doubleHopRoom,
        oneSlideJumpRoom, doubleSlideJumpRoom, oneJumpRoom, dropFloorRoom;

    [SerializeField]
    private GameObject currentRoom, oldRoom;

    [SerializeField]
    private int roomCounter = 0, roomCountCap, levelCounter, baseRoomCountMinimum = 5, levelMulptiplier = 10;

    private bool hasEndRoomSpawned = false;

    

    // Start is called before the first frame update
    void Awake()
    {
        roomCounter = 0;
        instance = this;

        roomList = new List<GameObject>();
        roomList.Add(startRoom);
        AddRoomCounter();
        //Set the room counter to 0 and add 1 for the start room



        //Sets the current room fro generating rooms later
        currentRoom = startRoom;

        specialRooms = new List<GameObject>();
        specialRooms.Add(ascendingRoom);
        specialRooms.Add(descendingRoom);
        specialRooms.Add(risingRoom);


        normalRooms = new List<GameObject>();
        normalRooms.Add(baseRoom);
        normalRooms.Add(oneHopRoom);
        normalRooms.Add(doubleHopRoom);
        normalRooms.Add(oneSlideJumpRoom);
        normalRooms.Add(doubleSlideJumpRoom);
        normalRooms.Add(oneJumpRoom);
        normalRooms.Add(dropFloorRoom);

        

        hasEndRoomSpawned = false;

        runDataValues.levelCount++;
        levelCounter = runDataValues.levelCount;

    }

    void Start()
    {
        for(int i=0; i<2; i++)
        {
            GenerateRoom();
        }

        roomCountCap = levelCounter < 1 ? baseRoomCountMinimum : (levelCounter - 1) * levelMulptiplier + baseRoomCountMinimum;
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

        if(hasEndRoomSpawned)
        {
            return;
        }

        //Change to UnityEngine.Random.Value
        float roomChooser = UnityEngine.Random.value;
        GameObject newRoom;

        //Reset back to 0.1f after testing
        if(roomCounter==roomCountCap-1)
        {

            newRoom = Instantiate(endRoom);
            hasEndRoomSpawned = true;
        }
        else
        {
            if (roomChooser < 0.1)
            {
                //UnityEngine.Random Special rooms
                newRoom = Instantiate(GenerateSpecialRoom());
            }
            else
            {
                newRoom = Instantiate(GenerateNormalRoom());
            }
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
        if(roomChooser<0.9f)
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
        AddRoomCounter();
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
        
        return specialRooms[(int)Mathf.Floor(UnityEngine.Random.Range(0,specialRooms.Count))];
        //Line below should be commented and the line above should be uncommented after testing
        //return specialRooms[0];

        
    }


    //selects a room UnityEngine.Randomly from the normal room list
    private GameObject GenerateNormalRoom()
    {
        
        return normalRooms[(int)Mathf.Floor(UnityEngine.Random.Range(0, normalRooms.Count))];
        //return normalRooms[0];
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

        //Debug.Log("Index Found at: "+ index+"\nGenerating at:"+trick.transform.position);


    }

    public void AddRoomCounter()
    {
        roomCounter++;
    }
}
