using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsterSpawnerScript : MonoBehaviour
{
    [SerializeField]
    protected GameObject spawningObject;

    protected float SpawnChance;
    
    protected int roomMinimum;
    protected float baseSpawnChance, increaseRate;


    protected void SpawnObject()
    {

        int roomsCleared = UIManager.instance.GetRoomsCleared();

        if (roomsCleared >= roomMinimum)
        {
            SpawnChance = baseSpawnChance + increaseRate * roomsCleared;
            if (Random.value < SpawnChance)
            {
                Transform parentTrans = this.GetComponent<Transform>();

                GameObject spawn = Instantiate(spawningObject, parentTrans.position, parentTrans.rotation);
                EnemyManager.instance.AddEnemy(spawn.GetComponent<EnemyParent>());
            }
        }
    } 
}
