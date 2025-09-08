using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private RunDataSO runValues;

    [SerializeField]
    private TMP_Text scoreTextObject, timeTextObject, roomsTextObject;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextObject.text = "Score: " + runValues.score;
        timeTextObject.text = "Time survived: " + runValues.timeMinutes + " mins " + runValues.timeSeconds + "." + runValues.timeMillis + " secs";
        roomsTextObject.text = "Rooms Cleared: " + runValues.roomsCleared;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
