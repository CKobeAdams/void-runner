using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDataInteracter : MonoBehaviour
{
    [SerializeField]
    private RunDataSO runDataValues;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //get values
    public int GetRoomsCleared()
    {
        return runDataValues.roomsCleared;
    }

    public int GetScore()
    {
        return runDataValues.score;
    }

    public int GetTimeMinutes()
    {
        return runDataValues.timeMinutes;
    }

    public int GetTimeSeconds()
    {
        return runDataValues.timeSeconds;
    }

    public int GetTimeMillis()
    {
        return runDataValues.timeMillis;
    }

    //setters
    public void SetRoomsCleared(int rooms)
    {
        runDataValues.roomsCleared = rooms;
    }

    public void SetScore(int score)
    {
        runDataValues.score = score;
    }

    public void SetTimeMinutes(int minutes)
    {
        runDataValues.timeMinutes = minutes;
    }

    public void SetTimeSeconds(int seconds)
    {
        runDataValues.timeSeconds = seconds;
    }
    public void SetTimeMillis(int millis)
    {
        runDataValues.timeMillis = millis;
    }
}
