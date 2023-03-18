public interface ITriggeredObject
{
    public void OnObjectTriggerEnter(PlayerController player, PlayerState playerState);

    public void OnObjectTriggerExit(PlayerController player, PlayerState playerState)
    {
    }
}