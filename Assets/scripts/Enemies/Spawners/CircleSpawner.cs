using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MobsterSpawnerScript
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnChance = 0.20f;

        //original value is 10
        roomMinimum = 10;

        //original value is 0.1
        baseSpawnChance = 0.1f;
        increaseRate = 1f / 290f;
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
