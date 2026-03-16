using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public static AnimatorManager instance { get; private set; }


    private Animator animator;

    [SerializeField]
    private GameObject playerSprite;

    private bool startUpState;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        startUpState = false;
        animator = playerSprite.GetComponent<Animator>();
        IdlingTurnOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdlingTurnOn()
    {
        animator.SetBool("Idling", true);
        
    }

    public void StartUpTurnOn()
    {
        //startUpState = true;
        animator.SetBool("StartUp", true);
    
    }

    public void AcceleratingTurnOn()
    {
        animator.SetBool("Accelerating", true);
     
    }

    public void DecceleratingTurnOn()
    {
        animator.SetBool("Deccelerating", true);
    }

    public void TopSpeedTurnOn()
    {
        animator.SetBool("TopSpeed", true);
    }

    public void TurningTurnOn()
    {
        animator.SetBool("Turning", true);
    }

    public void ResetAnimatorTriggers()
    {
        animator.SetBool("StartUp", false);
        animator.SetBool("Accelerating", false);
        animator.SetBool("Deccelerating", false);
        animator.SetBool("Turning", false);
        animator.SetBool("TopSpeed", false);
        animator.SetBool("Idling", false);
    }


}
