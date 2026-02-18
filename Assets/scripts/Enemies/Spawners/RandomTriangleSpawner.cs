using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTriangleSpawner : MobsterSpawnerScript
{
    //is a decimal out of 1
    private float SpawnChance = 0.20f;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.value < 0.20)
        {
            SpawnObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
