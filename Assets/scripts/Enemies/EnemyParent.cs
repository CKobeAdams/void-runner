using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    protected float health = 3, movementSpeed;
    protected bool isDead = false;
    protected int attackingDamage = 1, scoreValue = 300, threadValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damageTaken, bool adjustScore)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            isDead = true;
            EnemyManager.instance.RemoveEnemy(this.GetComponent<EnemyParent>());
            if (adjustScore)
            {
                UIManager.instance.AdjustScore(scoreValue);
                UIManager.instance.AdjustThreads(threadValue);
            }

            Debug.Log("Enemy Taking Damage: "+this.name);

        }

        //Debug.Log("Taking Damage");
    }

    protected virtual void CollisionDamageCheck()
    {
        if (!PlayerController.instance.GetPlayerDeathState())
        {
            if (this.GetComponent<PolygonCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                PlayerController.instance.TakeDamage(attackingDamage);
                TakeDamage(attackingDamage, false);
            }
        }
    }

    public PolygonCollider2D GetCollider()
    {
        return this.GetComponent<PolygonCollider2D>();
    }
}
