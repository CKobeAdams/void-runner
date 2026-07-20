using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : EnemyParent
{
    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
        scoreValue = 100;
        movementSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        CollisionDamageCheck();
        UpdateDeathRattle();
    }
}
