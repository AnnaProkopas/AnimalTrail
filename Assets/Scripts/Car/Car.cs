using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MovableObject, ITriggeredObject
{
    private TriggeredObjectType type = TriggeredObjectType.Car;

    public void OnObjectTriggerEnter(Player player, PlayerState playerState) 
    {
        player.OnStartTakingDamage(12);
    }
    
    public void OnObjectTriggerExit(Player player, PlayerState playerState) 
    {
        player.OnFinishTakingDamage();
    }

    void Start()
    {
        direction = new Vector2(1, 0);
    }
}
