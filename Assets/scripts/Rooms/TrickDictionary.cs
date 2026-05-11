using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrickDictionary : MonoBehaviour
{
    public static TrickDictionary instance { get; private set; }

    [SerializeField]
    private GameObject tickBox;

    private Dictionary<(string currentRoom, string prevRoom), (Vector3 location, Func<Vector3> piecewise)> trickFunctionDictionary 
        = new Dictionary<(string currentRoom, string prevRoom),(Vector3, Func<Vector3>)>();
    //private Dictionary<(string currentRoom, string prevRoom), Vector3>       trickLocationDictionary= new Dictionary<(string currentRoom, string prevRoom), Vector3>();

    private const string ascendingTag = "Ascension", selfTag = "Self", baseTag = "Base", oneJumpTag = "OneJump";
    private Vector3 ascendingFromBaseLocation = new Vector3(-23f, 11f, 0f),
                      OneJumpFromSelfLocation = new Vector3(2.5f, 0.5f, 0f),
                      baseFromSelfLocation = new Vector3(0f, 0.5f, 0f);
                                                                                                     
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
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


    }

    public (Vector3 location, Func<Vector3> piecewise) CiteValue((string currentRoom, string prevRoom) index)
    {
        (Vector3, Func<Vector3>) value = (new Vector3(0f, 0f, 0f), FailToFind);

        
        value = trickFunctionDictionary[index];
        
        return value;
    }

    public Func<Vector3> CiteFucntion((string currentRoom, string prevRoom) index)
    {
        Func<Vector3> funct = FailToFind;

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

    private Vector3 FailToFind()
    {
        return new Vector3(0f,0f,0f);
    }


    //The functions below are the core functions of the movement of the tricks
    private Vector3 OneJumpFromSelf()
    {
        Debug.Log("Flip off the box! Jump room trick");
        return new Vector3(1f,0f,0f);
    }
    private Vector3 AscendingFromBaseRoom()
    {
        Debug.Log("Flip off the top paltform! Ascneding Room trick");
        return new Vector3(1f, 0f, 0f);

    }

    private Vector3 BaseFromSelf()
    {
        Debug.Log("Doin Cool tricks in the base room!");
        return new Vector3(1f, 0f, 0f);
    }

    




}
