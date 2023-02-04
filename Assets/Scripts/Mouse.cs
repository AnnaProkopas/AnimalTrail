using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MovableObject
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public int energyPoints = 5;
    [SerializeField]
    public int healthPoints = 1;

    Vector2 directionSign;
    float currentSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        movement = directionSign * currentSpeed;
        movement.x = Mathf.Max(0, movement.x);

        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        animator.SetFloat("Speed", absX + absY);

        if (absX > absY) {
            animator.SetFloat("DirectionX", Mathf.Sign(movement.x));
        } else {
            animator.SetFloat("DirectionY", Mathf.Sign(movement.y));
        }
    }

    public void RunAwayFrom(Vector2 playerPosition) {
        direction = rb.position - playerPosition;
        directionSign.x = Mathf.Sign(direction.x);
        directionSign.y = Mathf.Sign(direction.y);
        currentSpeed = speed;
    }

    public void FreezeFromFear() {
        currentSpeed = 0;
    }
}
