using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTriangleSpawner : MobsterSpawnerScript
{
    //is a decimal out of 1
    

    void Start()
    {
        
        SpawnChance = 0.20f;

        //original Value is 50
        roomMinimum = 50;

        //original value is 0.05
        baseSpawnChance = 0.05f;
        increaseRate = 1f / 250f;

        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
