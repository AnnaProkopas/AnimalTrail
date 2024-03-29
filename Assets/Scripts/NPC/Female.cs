using JetBrains.Annotations;
using UnityEngine;

public class Female : MonoBehaviour, IPlayerTriggered
{
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField] 
    private GameObject cake;
        
    [CanBeNull] private Player observedObject;

    private FemaleState state = FemaleState.Idle;
    private int countCakes = 1;
    
    private const string AnimatorAttributeState = "state";
    private const string AnimatorAttributeRight = "right";

    void Update()
    {
        switch (state)
        {
            case FemaleState.Idle:
                animator.SetInteger(AnimatorAttributeState, 0);
                break;
            case FemaleState.Cry:
                animator.SetInteger(AnimatorAttributeState, 1);
                break;
            case FemaleState.Happy:
                animator.SetInteger(AnimatorAttributeState, 2);
                break;
        }

        if (observedObject)
        {
            Vector2 direction = observedObject.GetPosition() - rb.position;
            animator.SetBool(AnimatorAttributeRight, direction.x > 0);
        }
    }
    
    public void OnPlayerTriggerEnter(Player player, PlayerState playerState)
    {
        observedObject = player;
        
        switch (playerState)
        {
            case PlayerState.Attack:
                state = FemaleState.Cry;
                break;
            case PlayerState.LookAround:
                // player.onAttack -= OnAttack;
                state = FemaleState.Happy;
                if (countCakes-- > 0)
                {
                    Spawn(player.GetPosition() + (new Vector2(0, 1)));
                }
                break;
            default:
                state = FemaleState.Idle;
                break;
        } 
        
        player.onAttack += OnAttack;
    }

    public void OnPlayerTriggerExit(Player player, PlayerState playerState)
    {
        state = FemaleState.Idle;
        player.onAttack -= OnAttack;
    }

    private CollisionResult OnAttack()
    {
        state = FemaleState.Cry;
        CollisionResult res = new CollisionResult();
        res.healthPoints = 0;
        res.energyPoints = 0;
        return res;
    }

    private void Spawn(Vector2 position) 
    {
        Instantiate(cake, position, Quaternion.identity);
    }
}
