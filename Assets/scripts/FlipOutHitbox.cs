using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOutHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    public static FlipOutHitbox instance { get; private set; }

    void Start()
    {
        instance = this;
    }

    void awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
