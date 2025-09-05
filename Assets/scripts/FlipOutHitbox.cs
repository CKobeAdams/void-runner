using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOutHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    public static FlipOutHitbox instance { get; private set; }


    Color flipShade, startShade;
    [SerializeField]
    float CurrentDamage, startDamage;
    SpriteRenderer sRender;
    const int sourceCode = 0;

    float ssR, ssG, ssB;

    void Start()
    {
        

        startShade = new Color(1.0f / 255f, 185f / 255f, 255f / 255f, 1f);
        flipShade = startShade;
        ssR = 1.0f / 255f;
        ssG = 225f / 255f;
        ssB = 185f / 255f;

        CurrentDamage = 0.0f;
        startDamage = 0.0f;

    }

    void Awake()
    {

        sRender = this.GetComponent<SpriteRenderer>();

        instance = this;





    }

    // Update is called once per frame
    void Update()
    {
        if(ssG > 0)
        {
            ssG = ssG - 1f / 255f;
            flipShade = new Color(ssR, ssG, ssB, 1f);
        }
        else if(ssR<1.0)
        {
            ssR = ssR + 1f / 255f;
            flipShade = new Color(ssR, ssG, ssB, 1f);
        }
        else if(ssB>0)
        {
            ssB = ssB - 1f / 255f;
            flipShade = new Color(ssR, ssG, ssB, 1f);
        }

        CurrentDamage = CurrentDamage + 3f;
        sRender.color = flipShade;

        if(this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            EnemyManager.instance.EntityHurtSearch(this.GetComponent<BoxCollider2D>(), CurrentDamage, sourceCode);
           
        }

    }

    public void ResetBox()
    {
        
        ssR = startShade.r;
        ssG = startShade.g;
        ssB = startShade.b;
        CurrentDamage = startDamage;

    }

    public BoxCollider2D GetCollider()
    {
        return this.GetComponent<BoxCollider2D>();
    }
}
