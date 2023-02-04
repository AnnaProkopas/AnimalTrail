using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb;
    
    [SerializeField]
    protected float speed = 1;

    protected Vector2 direction = new Vector2(0, 0);
    protected Vector2 movement;

    // Update is called once per frame
    protected void Update()
    {
        movement = direction * speed;
    }
    
    protected void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   
}
