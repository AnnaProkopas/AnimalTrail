using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MovableObject, ITriggeredObject
{
    private TriggeredObjectType type = TriggeredObjectType.Car;

    public void OnObjectTriggerEnter(PlayerController player, PlayerState state) 
    {
        player.OnStartTakingDamage(12);
    }
    
    public void OnObjectTriggerExit(PlayerController player, PlayerState state) 
    {
        player.OnFinishTakingDamage();
    }

    void Start()
    {
        direction = new Vector2(1, 0);
    }
}
