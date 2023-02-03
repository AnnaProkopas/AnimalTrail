using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // public Transform player;
    public Rigidbody2D rb;
    public Joystick joystick;
    public Joybutton joybutton;
    public Text healthText;
    public Text foodCounterText;
    public EnergyManager energy;
    
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;

    Vector2 movement;
    bool isAttackModeOn = false;
    int health = 10;
    bool isDied = false;
    bool isHitted = false;
    int foodCounter = 0;
    int maxFoodCounter;

    const int MAX_HEALTH = 10;

    private void Start() {
        Load();
    }

    void Update() {
        if (!isDied) {
            movement.x = Mathf.Sign(joystick.Horizontal) * (Mathf.Abs(joystick.Horizontal) > .2f ? 1 : 0) * speed;
            movement.y = Mathf.Sign(joystick.Vertical) * (Mathf.Abs(joystick.Vertical) > .2f ? 1 : 0) * speed;


            if (joybutton.pressed) {
                isAttackModeOn = true;
            } else {
                isAttackModeOn = false;
                animator.SetBool("Attack", false);
            }

            healthText.text = "" + health;
            
            if (energy.GetEnergyValue() == 0) {
                isDied = true;
            }

            if (isDied) {
                animator.SetBool("Died", true);
            } else if (isAttackModeOn || isHitted) {
                animator.SetBool("Attack", true);
            } else {
                animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
                animator.SetFloat("DirectionX", Mathf.Sign(movement.x));
            }

            isHitted = false;
        }
    }

    void FixedUpdate() {
        if (!isDied) {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }   

    public bool IsAttackMode() {
        return isAttackModeOn;
    }

    public void Eat(int energyPoints, int healthPoints) {
        energy.AddEnergy(energyPoints);
        health = Math.Min(MAX_HEALTH, health + healthPoints);
    }

    
    void OnTriggerEnter2D (Collider2D other) {
        Mouse mouse = other.gameObject.GetComponent<Mouse>();
        if (mouse != null) {
            if (isAttackModeOn) {
                Eat(mouse.energyPoints, mouse.healthPoints);
                Destroy(other.gameObject);

                icreaseFoodCounter();
            } else {
                mouse.RunAwayFrom(rb.position);
            }
        }
        Car car = other.gameObject.GetComponent<Car>();
        if (car != null) {
            health -= 3;
            isDied = health == 0;
            isAttackModeOn = true;
            isHitted = true;
        } else {
            CarSnackSpawner cSS = other.gameObject.GetComponent<CarSnackSpawner>();
            if (cSS != null) {
                cSS.Spawn(rb.position + (new Vector2(2, 0)));
            }
        }
        Cake cake = other.gameObject.GetComponent<Cake>();
        if (cake != null) {
            Eat(cake.energyPoints, cake.healthPoints);
            Destroy(other.gameObject);

            icreaseFoodCounter();
        }
    }

    private void icreaseFoodCounter() {
        foodCounter++;
        maxFoodCounter = Math.Max(foodCounter, maxFoodCounter);
        foodCounterText.text = "" + foodCounter + (maxFoodCounter > foodCounter ? ("(" + maxFoodCounter + ")") : "");
        Save();
    }

    public void goToHomeScene() {
        Destroy(this.gameObject);
        SceneManager.LoadScene (0);
    }

    private void Load() {
        maxFoodCounter = PlayerPrefs.GetInt("maxFoodCounter", 0);
    }

    private void Save() {
        PlayerPrefs.SetInt("maxFoodCounter", maxFoodCounter);
    }
}
