using UnityEngine;

public class Cake : MonoBehaviour, IPlayerTriggered
{
    [SerializeField]
    public int energyPoints = 8;
    [SerializeField]
    public int healthPoints = -1;

    private TriggeredObjectType type = TriggeredObjectType.Cake;

    public void OnPlayerTriggerEnter(Player player, PlayerState playerState) 
    {
        switch (playerState)
        {
            case PlayerState.Dead:
            case PlayerState.Dying:
                break;
            default:
                player.Eat(energyPoints, healthPoints);
                Destroy(gameObject);
                break;
        }
    }
}
