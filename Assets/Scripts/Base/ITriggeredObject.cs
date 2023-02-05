
public interface ITriggeredObject
{
    public void OnObjectTriggerEnter(PlayerController player, PlayerState state);

    public void OnObjectTriggerExit(PlayerController player, PlayerState state)
    {
    }
}