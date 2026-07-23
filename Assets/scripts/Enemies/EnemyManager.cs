using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }



    private enum DamageSources
    {
        player,
        deathWall,
    }
    
    [SerializeField]
    public List<EnemyParent> enemyList;

    [SerializeField]
    private GameObject threadText;


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

    public void EntityHurtSearch(BoxCollider2D searchBox, float damage, int source)
    {
        //BoxCollider2D flipBox = FlipOutHitbox.instance.GetCollider();

        foreach(EnemyParent enem in enemyList)
        {
            if(searchBox.IsTouching(enem.GetCollider())&&!enem.GetIsDead())
            {
                
                
                if(source == (int)DamageSources.player)
                {
                    enem.TakeDamage(damage, true);
                    ItemManager.instance.InvokeEvent_PlayerKillsEnemy();
                    CameraManager.instance.InitiateCameraShake();
                    GameObject newText = Instantiate(threadText, enem.transform.position, Quaternion.identity);
                    newText.GetComponent<ThreadFlasher>().ChangeText(enem.GetThreadValue());

                }
                else
                {
                    enem.TakeDamage(damage, false);
                }


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
