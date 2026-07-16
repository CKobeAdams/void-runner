using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrickDictionary : MonoBehaviour
{
    public static TrickDictionary instance { get; private set; }

    [SerializeField]
    private GameObject tickBox;

    private Dictionary<(string currentRoom, string prevRoom), (Vector3 location, Func<Vector3,Vector3> piecewise)> trickFunctionDictionary 
        = new Dictionary<(string currentRoom, string prevRoom),(Vector3, Func<Vector3, Vector3>)>();
    //private Dictionary<(string currentRoom, string prevRoom), Vector3>       trickLocationDictionary= new Dictionary<(string currentRoom, string prevRoom), Vector3>();

    private const string ascendingTag = "Ascension", selfTag = "Self", baseTag = "Base", oneJumpTag = "OneJump", risingTag = "Rising";
    private Vector3 ascendingFromBaseLocation = new Vector3(-23f, 12f, 0f),
                      OneJumpFromSelfLocation = new Vector3(-1.5f, 1f, 0f),
                      baseFromSelfLocation = new Vector3(0f, -3f, 0f),
                      risingFromSelfLocation = new Vector3(0f, -3f, 0f);
                                                                                                     
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void Awake()
    {
        instance = this;
        //trickFunctionDictionary = new Dictionary<(string currentRoom, string prevRoom), Func<Vector3>>();
        //trickLocationDictionary = new Dictionary<(string currentRoom, string prevRoom), Vector3>();
        //MAJOR NOTE
        //consider storing all of the relative coordinates of these functions in an offshore csv file to be read in later
        //it will make this file less messy and easy to edit in the future

        //One Jump to Self 
        trickFunctionDictionary.Add((oneJumpTag, selfTag),(OneJumpFromSelfLocation, OneJumpFromSelf));
        //trickLocationDictionary.Add((oneJumpTag, selfTag), new Vector3(2.5f, 0.5f, 0f));
        
        //Ascending Room to Base Platform
        trickFunctionDictionary.Add((baseTag, ascendingTag),(ascendingFromBaseLocation, AscendingFromBaseRoom));
        //trickLocationDictionary.Add((ascendingTag, baseTag), new Vector3(15f,30f,0f));

        trickFunctionDictionary.Add((baseTag, selfTag), (baseFromSelfLocation, BaseFromSelf));

        trickFunctionDictionary.Add((risingTag, selfTag), (risingFromSelfLocation, RisingFromSelf));

    }

    public (Vector3 location, Func<Vector3, Vector3> piecewise) CiteValue((string currentRoom, string prevRoom) index)
    {
        (Vector3, Func<Vector3, Vector3>) value = (new Vector3(0f, 0f, 0f), FailToFind);

        
        value = trickFunctionDictionary[index];
        
        return value;
    }

    public Func<Vector3, Vector3> CiteFucntion((string currentRoom, string prevRoom) index)
    {
        Func<Vector3, Vector3> funct = FailToFind;

        //funct = trickFunctionDictionary[index].piecewise;

        
        return funct;
    }

    public Vector3 CiteLocation((string currentRoom, string prevRoom) index)
    {
        Vector3 value = new Vector3(0f,0f,0f);

        

        if (trickFunctionDictionary.ContainsKey(index))
        {
            value = trickFunctionDictionary[index].location;
        }
        else
        {

        }

        return value;
    }

    private Vector3 FailToFind(Vector3 initialPosition)
    {
        return new Vector3(0f,0f,0f);
    }


    //The functions below are the core functions of the movement of the tricks
    private Vector3 OneJumpFromSelf(Vector3 initialPosition)
    {
        Vector3 currentPosition = PlayerController.instance.GetPlayerPosition();

        float endPosition = 10f, currentFunctionXPosition = currentPosition.x - initialPosition.x,
            gravMultiplier = 2f;

        Vector3 returnVelocity = new Vector3(0f, 0f, 0f), initialVelocity = new Vector3(24f,7.5f);
        float trickTimeElapsed = currentFunctionXPosition / initialVelocity.x;

        float gravityAccel = Physics.gravity.y * gravMultiplier;

        if (currentFunctionXPosition>= endPosition)
        {
            PlayerController.instance.SetIsTrickable(false);
            PlayerController.instance.SetHasTricked(false);

        }
        else
        {
            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + trickTimeElapsed * gravityAccel * 2);
        }
        
        //Debug.Log("Flip off the box! Jump room trick");
        

        

        return returnVelocity;
    }

    private Vector3 AscendingFromBaseRoom(Vector3 initialPosition)
    {
        //Movement function y=(-112/2025 * x^2 + 23/45*x + 11)
        //Derivative y = -224/2025*x + 23/45
        //End position x=22.5
        Vector3 returnVelocity = new Vector3(0f, 0f, 0f);

        Vector3 playerPosition = PlayerController.instance.GetPlayerPosition();
        float endPosition = 22.5f, currentFunctionXposition = playerPosition.x-initialPosition.x;
        float gravMultiplier = 4f;

        Vector2 initialVelocity = new Vector2(22.5f,25f);

        float gravityAccel = Physics.gravity.y * gravMultiplier;
        
        //Debug.Log("Calling The Ascending function");
        float trickTimeElapsed = currentFunctionXposition/initialVelocity.x;
        


        if (currentFunctionXposition >= endPosition)
        {
            //derivative of the first function
            /*float slope = (componentA*2) * currentFunctionXposition + componentB;
            Vector3 normailizedSlope = new Vector3(1f, slope, 0f);
            normailizedSlope.Normalize();
            returnVelocity = normailizedSlope * PlayerController.instance.GetPlayerSpeed();*/

            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + trickTimeElapsed * gravityAccel * 2);

            PlayerController.instance.SetIsTrickable(false);
            PlayerController.instance.SetHasTricked(false);
            

        }
        else
        {
            //these are derivatives of the first function
            /*float slope = (2*componentA) * currentFunctionXposition + componentB;
            Vector2 normailizedSlope = new Vector2(1f, slope);
            //normailizedSlope.Normalize();
            Debug.Log("Current function position = " + currentFunctionXposition+
                "\nPlayerSpeed= " + PlayerController.instance.GetPlayerSpeed()+
                "\nCurrent Function Step = " + currentFunctionXposition+
                "\nRelative Y = " + (playerPosition.y)+
                "\nWorldPosition Y = " + playerPosition.y+
                "\nslope = " + slope);*/
            //returnVelocity = new Vector3(initalVelocity);
            //returnVelocity = normailizedSlope * PlayerController.instance.GetPlayerSpeed() * gravMultiplier;
            //gravMultiplier = 1 + ((currentFunctionXposition / endPosition)  - (endPosition/2));
            /*returnVelocity = new Vector2(initialVelocity.x, Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2f)
                            + Mathf.Pow(Physics.gravity.y * gravMultiplier * 2 * currentFunctionXposition,2f)));*/


            //returnVelocity = new Vector3(returnVelocity.x, returnVelocity.y, 0f)


            //change ALLL of this over to kinematics
            //BECAUSE x velocity is constant we can use the current position as a timer

            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + trickTimeElapsed * gravityAccel * 2);


        }

        //Debug.Log("Flip off the top paltform! Ascneding Room trick");
        return returnVelocity;

    }

    private Vector3 BaseFromSelf(Vector3 initialPosition)
    {
        //This is the general layout for the trick functions 
        Vector3 currentPosition = PlayerController.instance.GetPlayerPosition();

        Vector3 returnVelocity = new Vector3(0f, 0f, 0f), initialVelocity = new Vector3(25f,0f,0f);

        float endPosition = 10f, currentFunctionXPosition = currentPosition.x - initialPosition.x;

        //the function

        returnVelocity = initialVelocity;


        if ( currentFunctionXPosition >= endPosition)
        {
            PlayerController.instance.SetIsTrickable(false);
            PlayerController.instance.SetHasTricked(false);

        }

        //Debug.Log("Doin Cool tricks in the base room!");
        return returnVelocity;
    }

    private Vector3 RisingFromSelf(Vector3 initialPosition)
    {
        //This trick is based on the Y position rather than the X position
        Vector3 currentPosition = PlayerController.instance.GetPlayerPosition();

        Vector3 returnVelocity = new Vector3(0f, 0f, 0f), initialVelocity = new Vector3(-15f, 25f, 0f);
        //float initialYJumpVelocity = 25f;


        float endPosition = 30f, currentFunctionYPosition = currentPosition.y - initialPosition.y;
        float gravityMinistep, gravityMultiplier = 3;
        float gravityAccel = Physics.gravity.y * gravityMultiplier;

        float firstStep = 3f, secondStep = 6.5f, thirdStep = 10f, fourthStep = 13.5f, fifthStep = 17f, sixthStep = 20.5f, seventhStep = 24f, eighthstep = 27.5f;

        

        if(currentFunctionYPosition <= firstStep)
        {
            //returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + trickTimeElapsed * gravityAccel * 2);
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition))) / -gravityAccel;
            Debug.Log("First step");

            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + timeElapsed*gravityAccel * 2);
        }
        else if (currentFunctionYPosition <= secondStep)
        {
            //calculate gravity based off distance from first step
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - firstStep))) / -gravityAccel;

            Debug.Log("second step");
            Debug.Log(timeElapsed);

            returnVelocity = new Vector3(-initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
            Debug.Log(returnVelocity);
        }
        else if(currentFunctionYPosition <= thirdStep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - secondStep))) / -gravityAccel;

            Debug.Log("third step");

            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition <= fourthStep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - thirdStep))) / -gravityAccel;

            Debug.Log("4rth step");

            returnVelocity = new Vector3(-initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition <= fifthStep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - fourthStep))) / -gravityAccel;

            Debug.Log("5th step");

            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition <= sixthStep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - fifthStep))) / -gravityAccel;

            Debug.Log("6th step");

            returnVelocity = new Vector3(-initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition <= seventhStep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - sixthStep))) / -gravityAccel;
            Debug.Log("7th step");


            returnVelocity = new Vector3(initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition <= eighthstep)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - seventhStep))) / -gravityAccel;

            Debug.Log(timeElapsed);

            returnVelocity = new Vector3(-initialVelocity.x, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else if(currentFunctionYPosition < endPosition)
        {
            float timeElapsed = (-initialVelocity.y + Mathf.Sqrt(Mathf.Pow(initialVelocity.y, 2) - 2 * (gravityAccel) * (currentFunctionYPosition - eighthstep))) / -gravityAccel;

            

            returnVelocity = new Vector3(0, initialVelocity.y + timeElapsed * gravityAccel * 2);
        }
        else
        {
            PlayerController.instance.SetIsTrickable(false);
            PlayerController.instance.SetHasTricked(false);
        }

        //Debug.Log(returnVelocity);
        return returnVelocity;


    }

    




}
