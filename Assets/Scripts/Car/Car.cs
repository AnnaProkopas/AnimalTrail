using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    
    [SerializeField]
    private float speed = 1;

    Vector2 direction;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        direction.y = 0;
        direction.x = 1;
    }

    // Update is called once per frame
    void Update()
    {
        movement = direction * speed;
        
    }
    
    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }   
}
