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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartUpTurnOn()
    {
        startUpState = true;
        animator.SetBool("startUp", startUpState);
    }

    public void StartUpTurnOff()
    {
        startUpState = false;
        animator.SetBool("startUp", startUpState);
    }
}
