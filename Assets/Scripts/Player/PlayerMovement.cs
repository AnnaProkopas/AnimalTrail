using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    void moveCharacter (Vector2 direction) {
        player.Translate(direction * speed * Time.deltaTime);

    }   
}
