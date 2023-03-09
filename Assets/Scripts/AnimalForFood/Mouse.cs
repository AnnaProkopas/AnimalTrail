using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MovableObject, ITriggeredObject
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public int energyPoints = 5;
    [SerializeField]
    public int healthPoints = 1;

    Vector2 directionSign;
    float currentSpeed = 0;

    private TriggeredObjectType type = TriggeredObjectType.Mouse;

    public void OnObjectTriggerEnter(PlayerController player, PlayerState state) 
    {
        switch (state)
        {
            case PlayerState.Attack:
                player.Eat(energyPoints, healthPoints);
                Destroy(this.gameObject);
                break;
            default:
                RunAwayFrom(player.GetPosition());
                player.onAttack += OnAttack;
                break;
        }
    }

    public void OnObjectTriggerExit(PlayerController player, PlayerState state)
    {
        player.onAttack -= OnAttack;
    }

    protected override void Update()
    {
        movement = directionSign * currentSpeed;

        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        animator.SetFloat("Speed", absX + absY);

        if (absX > absY) 
        {
            animator.SetFloat("DirectionX", Mathf.Sign(movement.x));
        } else {
            animator.SetFloat("DirectionY", Mathf.Sign(movement.y));
        }
    }

    private void RunAwayFrom(Vector2 playerPosition) 
    {
        direction = rb.position - playerPosition;
        directionSign.x = Mathf.Max(0, Mathf.Sign(direction.x));
        directionSign.y = Mathf.Sign(direction.y);
        currentSpeed = speed;
    }

    private CollisionResult OnAttack()
    {
        Destroy(this.gameObject);
        
        
        CollisionResult res = new CollisionResult();
        res.healthPoints = healthPoints;
        res.energyPoints = energyPoints;
        return res;
    }
}
