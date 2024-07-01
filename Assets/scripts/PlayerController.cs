using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public GameObject groundCheck;
    float movementSpeed, acceleration, maxMovementSpeed, currentVelocity, moveDirection, jumpVelocity = 5f;
    public bool isMoving, isGrounded, gravityAffected;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 100f;
        gravityAffected = true;
        maxMovementSpeed = 5f;
        currentVelocity = 0;
        rigidbody = GetComponent<Rigidbody2D>();
        isGrounded = false;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gravityAffected && !isGrounded)
        {
            rigidbody.velocity  = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y-acceleration*Time.deltaTime);
        }
    }

    public void MovementPerformed(InputAction.CallbackContext context)
    {
        rigidbody.velocity = new Vector2(maxMovementSpeed*context.ReadValue<float>(), rigidbody.velocity.y);

    }
}
