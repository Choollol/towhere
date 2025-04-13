
public class PauseMenuManager : MenuManager
{
    protected override void CloseMenu()
    {
        base.CloseMenu();

        EventMessenger.TriggerEvent(EventKey.UnpauseGame);
    }
}
