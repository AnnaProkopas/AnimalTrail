using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // public Transform player;
    public Rigidbody2D rb;
    public Joystick joystick;
    public Joybutton joybutton;
    public Text healthText;
    public Text energyText;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    Vector2 movement;
    bool attackModeOn = false;
    int heath = 10;
    int energy = 10;

    void Update() {
        movement.x = Mathf.Sign(joystick.Horizontal) * (Mathf.Abs(joystick.Horizontal) > .2f ? 1 : 0) * speed;
        movement.y = Mathf.Sign(joystick.Vertical) * (Mathf.Abs(joystick.Vertical) > .2f ? 1 : 0) * speed;


        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        animator.SetFloat("DirectionX", Mathf.Sign(movement.x));

        if (joybutton.pressed) {
            if (!attackModeOn) {
                attackModeOn = true;
                animator.SetBool("Attack", true);
            }
        } else {
            attackModeOn = false;
                animator.SetBool("Attack", false);
        }

        healthText.text = "" + heath;
        energyText.text = "" + energy;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   
}
