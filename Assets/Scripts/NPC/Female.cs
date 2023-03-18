
using JetBrains.Annotations;
using UnityEngine;

public class Female : MonoBehaviour, ITriggeredObject
{
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField] 
    private GameObject cake;
        
    [CanBeNull] private PlayerController observedObject;

    private FemaleState state = FemaleState.Idle;

    private int countCakes = 1;

    void Start()
    {
        
    }

    void Update()
    {
        switch (state)
        {
            case FemaleState.Idle:
                animator.SetInteger("state", 0);
                break;
            case FemaleState.Cry:
                animator.SetInteger("state", 1);
                break;
            case FemaleState.Happy:
                animator.SetInteger("state", 2);
                break;
        }

        if (observedObject)
        {
            Vector2 direction = observedObject.GetPosition() - rb.position;
            animator.SetBool("right", direction.x > 0);
        }
    }
    
    public void OnObjectTriggerEnter(PlayerController player, PlayerState playerState)
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

    public void OnObjectTriggerExit(PlayerController player, PlayerState playerState)
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
        Debug.Log("Female.onAttack");
        return res;
    }

    private void Spawn(Vector2 position) 
    {
        Instantiate(cake, position, Quaternion.identity);
    }
}
