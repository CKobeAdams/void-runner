using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    [SerializeField]
    public List<EnemyParent> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    void Awake()
    {
        instance = this;

        enemyList = new List<EnemyParent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EntityHurtSearch(BoxCollider2D searchBox, float damage)
    {
        //BoxCollider2D flipBox = FlipOutHitbox.instance.GetCollider();

        foreach(EnemyParent enem in enemyList)
        {
            if(searchBox.IsTouching(enem.GetCollider()))
            {
                //This magic number will need to be changed 
                enem.TakeDamage(damage);
                break;
            }
        }
    }

    public void AddEnemy(EnemyParent enem)
    {
        enemyList.Add(enem);
    }

    public void RemoveEnemy(EnemyParent enem)
    {
        enemyList.Remove(enem);
        Destroy(enem.gameObject);
    }
}
