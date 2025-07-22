using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    public static DeathFloor instance { get; private set; }

    [SerializeField]
    private const float deathFloorOffset = -15f;
    private float yPos;
    private bool playerKilled = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = PlayerController.instance.GetPlayerPosition();
        Vector3 floorPos = new Vector3(playerPos.x, yPos, 0);

        this.transform.position = floorPos;

        if(!playerKilled)
        {
            if(this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                playerKilled = true;
                PlayerController.instance.KillPlayer();
            }
        }
    }

    public void SetFloorPosition(float floorPosition)
    {
        yPos = floorPosition + deathFloorOffset;
    }
}