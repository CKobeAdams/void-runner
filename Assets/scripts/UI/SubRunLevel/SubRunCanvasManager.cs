using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SubRunCanvasManager : MonoBehaviour
{
    [SerializeField]
    private RunDataSO runValues;

    [SerializeField]
    private TMP_Text levelCompleted, scoreTextObject, totalTimeTextObject, timeToFinishTextObject, threadCountTextObject, roomsTextObject;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {

        timer = 0f;
        scoreTextObject.color = new Color(1f, 1f, 1f, 0);
        totalTimeTextObject.color = new Color(1f, 1f, 1f, 0);

        timeToFinishTextObject.color = new Color(1f, 1f, 1f, 0);

        threadCountTextObject.color = new Color(1f, 1f, 1f, 0);

        roomsTextObject.color = new Color(1f, 1f, 1f, 0);

        levelCompleted.text = "Level " + runValues.levelCount + " Completed!";

    }


    // Update is called once per frame
    void Update()
    {
        //use a timer to turn on the text, the timer will be checked with else ifs in reverse order
        if(timer>5f)
        {
            //move to the next scene
            //if(levelcount%3==0) then shop
            SceneManager.LoadScene("main run", LoadSceneMode.Single);

        }
        else if(timer > 3.5f)
        {
            roomsTextObject.color = new Color(1f, 1f, 1f, 1f);
            roomsTextObject.text = "Rooms Cleared: " + runValues.roomsCleared;
        }
        else if(timer > 3f)
        {
            threadCountTextObject.color = new Color(1f, 1f, 1f, 1f);
            threadCountTextObject.text = "Threads: " + runValues.threadCount;

        }
        else if(timer >2.5f)
        {
            totalTimeTextObject.color = new Color(1f, 1f, 1f, 1f);
            totalTimeTextObject.text = "Time survived: " + runValues.totalTimeMinutes + " mins " + runValues.totalTimeSeconds + "." + runValues.totalTimeMillis + " secs";

        }
        else if(timer>2f)
        {
            timeToFinishTextObject.color = new Color(1f, 1f, 1f, 1f);
            timeToFinishTextObject.text = "Time to Finish: " + runValues.runTimeMinutes + " mins " + runValues.runTimeSeconds + "." + runValues.runTimeMillis + " secs";

        }
        else if(timer>1.5f)
        {
            scoreTextObject.color = new Color(1f, 1f, 1f, 1f);
            scoreTextObject.text = "Score: " + runValues.score;

        }
        timer += Time.deltaTime;
    }
}
