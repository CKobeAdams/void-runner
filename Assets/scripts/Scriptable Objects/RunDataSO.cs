using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class RunDataSO : ScriptableObject
{
    public int roomsCleared, score, runTimeMinutes, runTimeSeconds, runTimeMillis, totalTimeMinutes, totalTimeSeconds, totalTimeMillis, threadCount;
    public float previousTime;

    public float totalTime
    {
        set {
            totalTime = value;
            totalTimeMinutes = (int)Mathf.Floor(totalTime / 60);
            totalTimeSeconds = (int)Mathf.Floor(totalTime % 60);
            totalTimeMillis = (int)Mathf.Floor((totalTime * 1000 % 1000) / 10);

        }
        get { return totalTime; }
    }

}
