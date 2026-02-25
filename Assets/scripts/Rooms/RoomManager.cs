using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance {get; private set; }

    [SerializeField]
    private GameObject basePlatform;



    public List<GameObject> roomList, specialRooms, normalRooms;

    [SerializeField]
    private GameObject baseRoom, startRoom, ascendingRoom, descendingRoom, risingRoom, randomRoom, oneHopRoom, doubleHopRoom,
        oneSlideJumpRoom, doubleSlideJumpRoom, oneJumpRoom, dropFloorRoom;

    [SerializeField]
    private GameObject currentRoom, oldRoom;

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
        specialRooms.Add(randomRoom);


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

        //Change to Random.Value
        float roomChooser = Random.value;
        GameObject newRoom;

        //Reset back to 0.1f after testing
        if(roomChooser < 0.1f)
        {
            //Random Special rooms
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

        //adds the starting room to a new room
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
            }

            
        }
    }

    //This method selects a room randomly from the special room list
    private GameObject GenerateSpecialRoom()
    {
        //uncomment line below after testing
        return specialRooms[(int)Mathf.Floor(Random.Range(0,specialRooms.Count))];

        
    }


    //selects a room randomly from the normal room list
    private GameObject GenerateNormalRoom()
    {
        return normalRooms[(int)Mathf.Floor(Random.Range(0, normalRooms.Count))];
    }
   
    
    //Us the general manager to determine when to spawn another room
    //When a room has been spawned generate obstacles that the player needs to jump/crouch over
}
