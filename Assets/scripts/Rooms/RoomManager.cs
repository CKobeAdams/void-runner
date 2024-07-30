using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance {get; private set; }

    [SerializeField]
    private GameObject basePlatform;



    public List<GameObject> roomList;

    [SerializeField]
    private GameObject baseRoom, startRoom, ascendingRoom, descendingRoom;

    [SerializeField]
    private GameObject currentRoom, oldRoom;

    // Start is called before the first frame update
    void awake()
    {
        

        
    }
    
    void Start()
    {
        instance = this;

        roomList = new List<GameObject>();
        roomList.Add(startRoom);

        currentRoom = startRoom;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentRoom();
        Vector3 floorPos = currentRoom.transform.GetChild(1).position;
        PlayerController.instance.SetMinimumCameraHeight(floorPos.y);
    }

    public void GenerateRoom()
    {
        //GameObject newRoom = Instantiate(baseRoom, GetListEndNode().transform);
        //makes the new room and gets starting node
        float roomChooser = Random.value;
        GameObject newRoom;

        if(roomChooser < 0.5f)
        {
            newRoom = Instantiate(baseRoom);
        }
        else if(roomChooser>0.75f)
        {
            newRoom = Instantiate(descendingRoom);
        }
        else
        {
            newRoom = Instantiate(ascendingRoom);
        }
        
        Transform newRoomStartNode = newRoom.transform.GetChild(0);

        //Gets the offset to the center of the room to the starting node
        Vector3 newRoomOffset =  newRoomStartNode.position - newRoom.transform.position;


        //places the starting node in the correct position
        Vector3 nodePosition = GetListEndNodePosition();
        Debug.Log(newRoomStartNode.position);
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

   
    
    //Us the general manager to determine when to spawn another room
    //When a room has been spawned generate obstacles that the player needs to jump/crouch over
}
