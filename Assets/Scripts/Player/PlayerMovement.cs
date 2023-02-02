using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public Transform player;
    public Rigidbody2D rb;
    public Joystick joystick;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    Vector2 movement;

    void Update() {
        movement.x = Mathf.Sign(joystick.Horizontal) * (Mathf.Abs(joystick.Horizontal) > .2f ? 1 : 0) * speed;
        movement.y = Mathf.Sign(joystick.Vertical) * (Mathf.Abs(joystick.Vertical) > .2f ? 1 : 0) * speed;


        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        animator.SetFloat("DirectionX", Mathf.Sign(movement.x));

    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   
}
