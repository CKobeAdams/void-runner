using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera MainCam;

    public static CameraManager instance { get; private set; }

    private bool CameraLockSetting, CameraLockStatus, isLingering = false;

    private float minimumCameraHeight = 0f, cameraFloorDistance = 5f, cameraSpeed = 6f, cameraRaiseLower, cameraLingerTimer = 0f;

    //this constant determines how long the camera lingers for
    private const float cameraLingerLimit = 2.5f;

    private Vector2 playerMoveVector, cameraDisplacement, previousMoveVector;
    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        CameraLockSetting = true;
        CameraLockStatus = true;

        cameraDisplacement = new Vector2(10f, 3f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        playerMoveVector = PlayerController.instance.GetMoveVector();

        //changes the Camera lock and against the status | used to be in PlayerController
        if (CameraLockSetting == CameraLockStatus)
        {
            if (CameraLockStatus)
            {
                MoveCamera();
            }
            else
            {
                MoveCamera();
            }
        }
        else
        {
            if (CameraLockSetting)
            {
                PlayerController.instance.GroundCheck();
                if (PlayerController.instance.GetIsGrounded())
                {

                    CameraLockStatus = true;
                    MoveCamera();
                }
                else
                {
                    MoveCamera();
                }
            }
            else
            {

                CameraLockStatus = false;
                MoveCamera();
            }
        }
        previousMoveVector = playerMoveVector;
    }

    public void MoveCamera()
    {
        Vector3 playerTrans = PlayerController.instance.GetPlayerTransform().position;
       
        Vector3 cameraHeight = new Vector3(playerTrans.x, 0f, -10f);
        

        float step = cameraSpeed * Time.deltaTime;

        if (CameraLockStatus)
        {
            cameraHeight.y = MainCam.transform.position.y;
        }
        else
        {
            cameraHeight.y = playerTrans.y;
        }

        if (cameraHeight.y < minimumCameraHeight)
        {
            cameraHeight.y = minimumCameraHeight;
        }

        float cameraLingerVector = 0;

        if(playerMoveVector.x == 1)
        {
            cameraLingerVector = 1;
            isLingering = true;
            cameraLingerTimer = 0f;

        }
        else
        {
            if(isLingering)
            {
                cameraLingerTimer += Time.deltaTime;
                cameraLingerVector = 1;
                if(cameraLingerTimer > cameraLingerLimit)
                {
                    isLingering = false;
                }
            }
            else
            {
                cameraLingerVector = playerMoveVector.x;
            }
        }

        Vector3 targetPosition = new Vector3(cameraHeight.x + (cameraDisplacement.x * cameraLingerVector),
                cameraHeight.y + (cameraDisplacement.y * playerMoveVector.y),
                MainCam.transform.position.z);


        /*MainCam.transform.position = new Vector3(
            Mathf.Clamp(cameraHeight.x, playerTrans.x-50f, playerTrans.x+50f), 
            Mathf.Clamp(cameraHeight.y, playerTrans.y-50f, playerTrans.y+50f), 
            MainCam.transform.position.z);*/


        if (playerMoveVector.x == 1)
        {
            step = 35f*Time.deltaTime;
        }
        else
        {
            step = 15f*Time.deltaTime;
        }

        MainCam.transform.position = Vector3.MoveTowards(MainCam.transform.position, targetPosition, step);

 
    }

    public bool GetLockSetting()
    {
        return CameraLockSetting;
    }

    public bool GetLockStatus()
    {
        return CameraLockStatus;
    }

    public void SetLockSetting(bool setting)
    {
        CameraLockSetting = setting;
    }

    public void SetLockStatus(bool status)
    {
        CameraLockStatus = status;
    }

    public void SetMinimumCameraHeight(float floorHeight)
    {
        minimumCameraHeight = floorHeight + cameraFloorDistance + cameraRaiseLower;
    }

    public void SetCameraRaiseLower(float difference)
    {
        cameraRaiseLower = difference;
    }



   
}
