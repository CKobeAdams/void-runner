using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class RunDataSO : ScriptableObject
{
    public int roomsCleared, score, runTimeMinutes, runTimeSeconds, runTimeMillis, totalTimeMinutes, totalTimeSeconds, totalTimeMillis, threadCount;
    public float previousTime, _totalTime;
    public int playerHealth = 3;
    

    public float totalTime
    {
        set {
            _totalTime = value;
            totalTimeMinutes = (int)Mathf.Floor(_totalTime / 60f);
            totalTimeSeconds = (int)Mathf.Floor(_totalTime % 60f);
            totalTimeMillis = (int)Mathf.Floor((_totalTime * 1000f % 1000f) / 10f);

        }
        get { return _totalTime; }
    }

}
