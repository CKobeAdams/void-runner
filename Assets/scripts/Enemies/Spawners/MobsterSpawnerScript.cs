using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsterSpawnerScript : MonoBehaviour
{
    [SerializeField]
    protected GameObject spawningObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SpawnObject()
    {
        GameObject spawn = Instantiate(spawningObject);
        EnemyManager.instance.AddEnemy(spawn.GetComponent<EnemyParent>());
     
    }
}
