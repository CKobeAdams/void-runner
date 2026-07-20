using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera MainCam;

    public static CameraManager instance { get; private set; }

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private bool CameraLockSetting, CameraLockStatus, isLingering = false, trickLock = false;

    [SerializeField]
    private float minimumCameraHeight = 0f, cameraFloorDistance = 5f, cameraSpeed = 6f, cameraRaiseLower, cameraLingerTimer = 0f;

    private float cameraShakeDuration = 0.5f, cameraShakeStrength = 1;

    //this constant determines how long the camera lingers for
    private const float cameraLingerLimit = 0f;

    private Vector2 playerMoveVector, cameraBaseDisplacement, previousMoveVector, cameraMoveOffset;
    private Vector3 cameraMovingStep, previousPosition;

    public bool tester = false;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        CameraLockSetting = true;
        CameraLockStatus = true;

        cameraBaseDisplacement = new Vector2(10f, 3f);
        cameraMoveOffset = new Vector2(1f, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        cameraMovingStep = MainCam.transform.position - previousPosition;
        previousPosition = MainCam.transform.position;
        playerMoveVector = PlayerController.instance.GetMoveVector();

    }

    void FixedUpdate()
    {
        

        

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
       
        Vector3 cameraHeight = new Vector3(playerTrans.x + cameraBaseDisplacement.x, 0f, -10f);
        

        float step = cameraSpeed * Time.deltaTime;

        //The below if and if/else statements can be written more efficiently

        if (CameraLockStatus&&!trickLock)
        {
            cameraHeight.y = minimumCameraHeight;
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

        Vector3 targetPosition = new Vector3(cameraHeight.x + (cameraMoveOffset.x * cameraLingerVector),
                cameraHeight.y + (cameraMoveOffset.y * playerMoveVector.y),
                MainCam.transform.position.z);


        /*MainCam.transform.position = new Vector3(
            Mathf.Clamp(cameraHeight.x, playerTrans.x-50f, playerTrans.x+50f), 
            Mathf.Clamp(cameraHeight.y, playerTrans.y-50f, playerTrans.y+50f), 
            MainCam.transform.position.z);*/


        if (!trickLock)
        {
            step = 35f*Time.deltaTime;
        }
        else
        {
            step = 55f*Time.deltaTime;
        }

        
        MainCam.transform.position = Vector3.MoveTowards(MainCam.transform.position, targetPosition, step);

 
    }

    public void InitiateCameraShake(float duration = 0.15f, float strength = 1f)
    {
        cameraShakeStrength = strength;
        cameraShakeDuration = duration;
        StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < cameraShakeDuration)
        {
            elapsedTime += Time.deltaTime;

            Vector2 endPosition = startPosition + Random.insideUnitCircle * cameraShakeStrength;

            transform.position = new Vector3(endPosition.x, endPosition.y, transform.position.z);

            yield return null;
        }

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

    public float GetCameraPixelHeight()
    {
        return MainCam.pixelHeight;
    }

    public float GetCameraPixelWidth()
    {
        return MainCam.pixelWidth;
    }

    public float GetCameraSize()
    {
        return MainCam.orthographicSize*2;
    }

    public Vector3 GetCameraPosition()
    {
        return MainCam.transform.position;
    }

    public Vector2 GetCameraBaseDisplacement()
    {
        return cameraBaseDisplacement;
    }

    public Vector2 GetCameraMovingDisplacement()
    {
        return new Vector2(cameraMovingStep.x, cameraMovingStep.y);
    }

    public void SetTrickLock(bool setting)
    {
        trickLock = setting;
    }
   
}
