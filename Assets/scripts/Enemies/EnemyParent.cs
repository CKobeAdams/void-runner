using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    protected float health = 3, movementSpeed;
    protected bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damageTaken)
    {
        health = health - damageTaken;
    }

    public PolygonCollider2D GetCollider()
    {
        return this.GetComponent<PolygonCollider2D>();
    }
}
