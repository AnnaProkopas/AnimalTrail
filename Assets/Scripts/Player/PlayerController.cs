using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text foodCounterText;
    [SerializeField]
    private EnergyManager energy;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;

    private PlayerState currentState;
    private Vector2 movement;

    private PlayerRatingService ratingService;
    private int foodCounter = 0;
    private int recordValueForFoodCounter;

    private int health = 10;
    private const int MaxHealth = 10;

    private void Start()
    {
        ratingService = new PlayerRatingService();
        recordValueForFoodCounter = ratingService.GetRecordFoodCounter();
    }

    private void Update()
    {
        switch (currentState)
        {
        case PlayerState.Dead:
            return;
        case PlayerState.Dying:
            movement = new Vector2(0, 0);
            animator.SetInteger("State", (int)PlayerState.Dead);
            return;
        }

        movement.x = Mathf.Sign(joystick.Horizontal) * (Mathf.Abs(joystick.Horizontal) > .2f ? 1 : 0) * speed;
        movement.y = Mathf.Sign(joystick.Vertical) * (Mathf.Abs(joystick.Vertical) > .2f ? 1 : 0) * speed;
        float absMovement = Mathf.Abs(movement.x) + Mathf.Abs(movement.y);

        animator.SetInteger("State", (int)currentState);
        animator.SetFloat("Speed", absMovement);
        animator.SetFloat("DirectionX", Mathf.Sign(movement.x));
    }

    private void FixedUpdate() 
    {
        if (currentState != PlayerState.Dead) 
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D (Collider2D other) 
    {
        ITriggeredObject otherObject = other.gameObject.GetComponent<ITriggeredObject>();
        if (otherObject != null) 
        {
            otherObject.OnObjectTriggerEnter(this, currentState);
        }
    }

    private void OnTriggerExit2D (Collider2D other) 
    {
        ITriggeredObject otherObject = other.gameObject.GetComponent<ITriggeredObject>();
        if (otherObject != null) 
        {
            otherObject.OnObjectTriggerExit(this, currentState);
        }
    }

    private void IncreaseFoodCounter() 
    {
        foodCounter++;
        recordValueForFoodCounter = Math.Max(foodCounter, recordValueForFoodCounter);
        foodCounterText.text = "" + foodCounter + (recordValueForFoodCounter > foodCounter ? ("(" + recordValueForFoodCounter + ")") : "");
        ratingService.SetRecordFoodCounter(recordValueForFoodCounter);
    }

    private bool IsReadyForDeath()
    {
        return health == 0 || energy.GetEnergyValue() == 0;
    }

    private void StartDyingProcess()
    {
        if (currentState != PlayerState.Dead) currentState = PlayerState.Dying;
    }

    private void IfNotDyingSetState(PlayerState state)
    {
        switch(currentState)
        {
        case PlayerState.Dead:
        case PlayerState.Dying:
            break;
        default:
            currentState = state;
            break;
        }
    }

    private void ChangeHealth(int value) 
    {
        health = Math.Min(Math.Max(health + value, 0), MaxHealth);
        healthText.text = "" + health;
        if (IsReadyForDeath())
        {
            StartDyingProcess();
        }
    }

    public Vector2 GetPosition() 
    {
        return rb.position;
    }

    public void GoToHomeScene() 
    {
        switch (currentState)
        {
            case PlayerState.Dead:
            case PlayerState.Dying:
                ratingService.AddRecord(foodCounter);
                break;
            default:
                break;
        }

        Destroy(this.gameObject);
        SceneManager.LoadScene (0);
    }

    public void Eat(int energyPoints, int healthPoints) 
    {
        energy.Add(energyPoints);
        ChangeHealth((healthPoints));
        IncreaseFoodCounter();
    }

    public void OnEnergyIsOver()
    {
        StartDyingProcess();
    }

    public void OnStartTakingDamage(int damage)
    {
        ChangeHealth(-damage);
        IfNotDyingSetState(PlayerState.Wounded);
    }

    public void OnFinishTakingDamage()
    { 
        IfNotDyingSetState(PlayerState.Idle);
    }

    public void EnableAttackMode()
    {
        IfNotDyingSetState(PlayerState.Attack);
    }

    public void DisableAttackMode()
    {
        IfNotDyingSetState(PlayerState.Idle);
    }
}
