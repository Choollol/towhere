using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    private void Awake()
    {
        EventMessenger.StartListening(EventKey.EnableInteractIndicator, Enable);
        EventMessenger.StartListening(EventKey.DisableInteractIndicator, Disable);

        Disable();
    }
    private void OnDestroy()
    {
        EventMessenger.StopListening(EventKey.EnableInteractIndicator, Enable);
        EventMessenger.StopListening(EventKey.DisableInteractIndicator, Disable);
    }
    private void Enable()
    {
        gameObject.SetActive(true);

        DataMessenger.SetBool(BoolKey.IsInteractIndicatorActive, true);
    }
    private void Disable()
    {
        gameObject.SetActive(false);

        DataMessenger.SetBool(BoolKey.IsInteractIndicatorActive, false);
    }
}
