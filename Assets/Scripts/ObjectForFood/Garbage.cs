using UnityEngine;

public class Garbage : MonoBehaviour, ITriggeredObject
{
    [SerializeField]
    public int energyPoints = 2;
    [SerializeField]
    public int healthPoints = -3;

    private TriggeredObjectType type = TriggeredObjectType.Garbage;

    public void OnObjectTriggerEnter(Player player, PlayerState playerState) 
    {
        switch (playerState)
        {
            case PlayerState.Dead:
            case PlayerState.Dying:
                break;
            case PlayerState.Attack:
                player.Eat(energyPoints, healthPoints);
                Destroy(gameObject);
                break;
        }
    }
}
