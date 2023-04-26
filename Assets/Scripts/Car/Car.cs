using UnityEngine;

public class Car : MovableObject, IPlayerTriggered, INPCAnimalTriggered
{
    private TriggeredObjectType type = TriggeredObjectType.Car;

    public void OnPlayerTriggerEnter(Player player, PlayerState playerState) 
    {
        player.OnStartTakingDamage(8);
    }
    
    public void OnPlayerTriggerExit(Player player, PlayerState playerState) 
    {
        player.OnFinishTakingDamage();
    }

    void Start()
    {
        direction = new Vector2(1, 0);
    }
    
    public void OnNpcAnimalTriggerEnter(INPCAnimal npcAnimal)
    {
        npcAnimal.Disappear();
    }
}
