using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour, ITriggeredObject
{
    [SerializeField]
    public int energyPoints = 8;
    [SerializeField]
    public int healthPoints = -1;

    private TriggeredObjectType type = TriggeredObjectType.Cake;

    public void OnObjectTriggerEnter(PlayerController player, PlayerState state) 
    {
        switch (state)
        {
            case PlayerState.Dead:
            case PlayerState.Dying:
                break;
            default:
                player.Eat(energyPoints, healthPoints);
                Destroy(this.gameObject);
                break;
        }
    }
}
