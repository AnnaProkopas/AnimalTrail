using System;
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
    public EnergyManager energy;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    Vector2 movement;
    bool isAttackModeOn = false;
    int heath = 10;

    const int MAX_HEALTH = 10;

    void Update() {
        movement.x = Mathf.Sign(joystick.Horizontal) * (Mathf.Abs(joystick.Horizontal) > .2f ? 1 : 0) * speed;
        movement.y = Mathf.Sign(joystick.Vertical) * (Mathf.Abs(joystick.Vertical) > .2f ? 1 : 0) * speed;


        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        animator.SetFloat("DirectionX", Mathf.Sign(movement.x));

        if (joybutton.pressed) {
            isAttackModeOn = true;
            animator.SetBool("Attack", true);
        } else {
            isAttackModeOn = false;
            animator.SetBool("Attack", false);
        }

        healthText.text = "" + heath;
        
        if (energy.GetEnergyValue() == 0) {
            animator.SetBool("Died", true);
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   

    public bool IsAttackMode() {
        return isAttackModeOn;
    }

    public void Eat(int energyPoints, int healthPoints) {
        energy.AddEnergy(energyPoints);
        heath = Math.Min(MAX_HEALTH, heath + healthPoints);
    }

    
    void OnTriggerEnter2D (Collider2D other) {
        Mouse mouse = other.gameObject.GetComponent<Mouse>();
        if (mouse != null) {
            if (isAttackModeOn) {
                Eat(mouse.energyPoints, mouse.healthPoints);
                Destroy(other.gameObject);
            } else {
                mouse.RunAwayFrom(rb.position);
            }
        }
    }
}
