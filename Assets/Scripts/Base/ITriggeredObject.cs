public interface ITriggeredObject
{
    public void OnObjectTriggerEnter(Player player, PlayerState playerState);

    public void OnObjectTriggerExit(Player player, PlayerState playerState)
    {
    }
}