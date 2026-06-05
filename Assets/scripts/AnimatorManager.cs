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

    public void SlidingTurnOn()
    {
        animator.SetTrigger("Sliding");
    }

    public void CrawlingTurnOn()
    {
        animator.SetTrigger("Crawling");
    }

    public void GroundedTurnOn()
    {
        animator.SetBool("Grounded", true);
    }

    public void JumpingTurnOn()
    {
        animator.SetBool("Jumping", true);
    }

    public void FallingTurnOn()
    {
        animator.SetBool("Falling", true);
    }

    public void FlipLeftTurnOn()
    {
        animator.SetTrigger("FlipLeft");
    }

    public void FlipRightTurnOn()
    {
        animator.SetTrigger("FlipRight");
    }

    public void ResetAnimatorTriggers()
    {
        animator.SetBool("StartUp", false);
        animator.SetBool("Accelerating", false);
        animator.SetBool("Deccelerating", false);
        animator.SetBool("Turning", false);
        animator.SetBool("TopSpeed", false);
        animator.SetBool("Idling", false);
        animator.SetBool("Falling",false);
        animator.SetBool("Jumping", false);
        animator.ResetTrigger("Sliding");
        animator.ResetTrigger("Crawling");
        animator.ResetTrigger("FlipRight");
        animator.ResetTrigger("FlipLeft");
    }

    public void ResetSlidingTrigger()
    {
        animator.ResetTrigger("Sliding");
    }

    public void TurnOffGrounded()
    {
        animator.SetBool("Grounded", false);
    }


}
