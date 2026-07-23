using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    protected float health = 3, movementSpeed, rattleCounter = 0;
    protected bool isDead = false, isRattling = false;
    protected int attackingDamage = 1, scoreValue = 300, threadValue = 10;
    protected float rattleTimer = 0.6f;

    [SerializeField]
    protected ParticleSystem rattleBurst;

    void Awake()
    {
        rattleBurst.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure that this line is included in every update frame
        UpdateDeathRattle();
    }

    public virtual void TakeDamage(float damageTaken, bool adjustScore)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            isDead = true;
            InitiateDeathRattle();
            if (adjustScore)
            {
                UIManager.instance.AdjustScore(scoreValue);
                UIManager.instance.AdjustThreads(threadValue);
            }

            //Debug.Log("Enemy Taking Damage: "+this.name);

        }

        //Debug.Log("Taking Damage");
    }

    protected virtual void CollisionDamageCheck()
    {
        if (!PlayerController.instance.GetPlayerDeathState()&&!isDead)
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

    protected virtual void InitiateDeathRattle()
    {
        isRattling = true;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.GetComponent<Rigidbody2D>().Sleep();
        rattleBurst.gameObject.SetActive(true);
    }

    protected virtual void UpdateDeathRattle()
    {
        if (isRattling)
        {
            if (rattleCounter <= rattleTimer)
            {
                rattleCounter += Time.deltaTime;
            }
            else
            {
                EnemyManager.instance.RemoveEnemy(this.GetComponent<EnemyParent>());
            }
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public int GetThreadValue()
    {
        return threadValue;
    }
}
