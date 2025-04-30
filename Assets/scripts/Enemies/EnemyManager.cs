using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EntityHurtSearch()
    {
        //Searches through the enemy list for an entity that collides with a hit box the player made
    }
}
