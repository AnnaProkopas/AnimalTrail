using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public int energyPoints = 5;
    [SerializeField]
    public int healthPoints = 1;
    [SerializeField]
    private float speed = 1;

    Vector2 movement;
    Vector2 direction;
    float currentSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        movement = direction * currentSpeed;
        movement.x = Mathf.Max(0, movement.x);

        float absX = Mathf.Abs(movement.x);
        float absY = Mathf.Abs(movement.y);

        animator.SetFloat("Speed", absX + absY);

        if (absX >= absY) {
            animator.SetFloat("DirectionX", Mathf.Sign(movement.x));
        } else {
            animator.SetFloat("DirectionY", Mathf.Sign(movement.y));
        }
    }

    public void RunAwayFrom(Vector2 playerPosition) {
        direction.x = Mathf.Sign(rb.position.x - playerPosition.x);
        direction.y = Mathf.Sign(rb.position.y - playerPosition.y);
        currentSpeed = speed;
    }

    public void FreezeFromFear() {
        currentSpeed = 0;
    }
    
    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   
}
