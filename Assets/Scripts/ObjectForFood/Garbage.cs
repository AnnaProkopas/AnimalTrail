using UnityEngine;

public class Garbage : MonoBehaviour, IPlayerTriggered
{
    [SerializeField]
    public int energyPoints = 2;
    [SerializeField]
    public int healthPoints = -3;

    private TriggeredObjectType type = TriggeredObjectType.Garbage;

    public void OnPlayerTriggerEnter(Player player, PlayerState playerState) 
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
            default:
                player.onAttack += OnAttack;
                break;
        }
    }

    public void OnPlayerTriggerExit(Player player, PlayerState state)
    {
        player.onAttack -= OnAttack;
    }

    private CollisionResult OnAttack()
    {
        Destroy(this.gameObject);

        CollisionResult res = new CollisionResult();
        res.healthPoints = healthPoints;
        res.energyPoints = energyPoints;
        return res;
    }
}
