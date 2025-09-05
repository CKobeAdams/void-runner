using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField]
    TMP_Text stopwatchObject, roomCounterObject, scoreText;

    [SerializeField]
    Canvas canvasObject;

    [SerializeField]
    GameObject healthBarObject;


    private float stopwatchCounter, scoreCounter;
    private int roomCounter;
    private bool timerPaused;
    private Vector2 renderingDisplaySize;


    // Later set up the UI to 
    // Start is called before the first frame update
    void Start()
    {
        stopwatchCounter = 0f;
        scoreCounter = 0f;
        roomCounter = 0;
        timerPaused = false;
        SetUIPositions();
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerPaused)
        {
            UpdateStopwatch();
        }
        
    }

    private void UpdateStopwatch()
    {
        stopwatchCounter += Time.deltaTime;
        int minutes = (int)Mathf.Floor(stopwatchCounter / 60);
        int seconds = (int)Mathf.Floor(stopwatchCounter % 60);
        int milliseconds = (int)Mathf.Floor((stopwatchCounter * 1000%1000)/10);
        string text = minutes.ToString("D2") + ":"+seconds.ToString("D2")+":"+milliseconds.ToString("D2");

        stopwatchObject.text = text;
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }

    public void AdjustScore(int addingScore)
    {
        scoreCounter += addingScore;
        string text = "Score: " + scoreCounter;

        scoreText.text = text;

    }

    public void SetUIPositions()
    {
        renderingDisplaySize = canvasObject.renderingDisplaySize;

        healthBarObject.transform.position = new Vector3(0.96f*renderingDisplaySize.x, 0.95f*renderingDisplaySize.y, 0f);
        stopwatchObject.transform.position = new Vector3(0.5f * renderingDisplaySize.x, 0.95f * renderingDisplaySize.y, 0f);
        roomCounterObject.transform.position = new Vector3(0.15f * renderingDisplaySize.x, 0.95f * renderingDisplaySize.y, 0f);
        scoreText.transform.position = new Vector3(0.1f * renderingDisplaySize.x, 0.88f * renderingDisplaySize.y, 0f);
    }

    public void RoomCleared()
    {
        roomCounter++;
        string text = "Rooms Cleared: " + roomCounter;

        roomCounterObject.text = text;
    }
}
