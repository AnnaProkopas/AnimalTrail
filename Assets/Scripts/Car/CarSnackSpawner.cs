using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSnackSpawner : MonoBehaviour, ITriggeredObject
{
    public GameObject cake;

    private TriggeredObjectType type = TriggeredObjectType.CarFoodSpawner;

    public void OnObjectTriggerEnter(PlayerController player, PlayerState playerState) 
    {
        switch (playerState)
        {
            case PlayerState.Dead:
            case PlayerState.Dying:
            case PlayerState.Attack:
                break;
            default:
                Spawn(player.GetPosition() + (new Vector2(2, 0)));
                break;
        }
    }

    private void Spawn(Vector2 position) 
    {
        Instantiate(cake, position, Quaternion.identity);
    }
}
