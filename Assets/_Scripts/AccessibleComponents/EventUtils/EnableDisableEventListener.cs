using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEventListener : MonoBehaviour
{
    [SerializeField] private string eventToListenFor;
    [SerializeField] private bool activeStatusToSwitchTo = true;

    private void Awake()
    {
        GetActiveStatusMethod(!activeStatusToSwitchTo)();
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(eventToListenFor, GetActiveStatusMethod(activeStatusToSwitchTo));
    }
    private void OnDisable()
    {
        EventMessenger.StartListening(eventToListenFor, GetActiveStatusMethod(activeStatusToSwitchTo));
    }
    private UnityAction GetActiveStatusMethod(bool targetActiveStatus)
    {
        return targetActiveStatus ? gameObject.Enable : gameObject.Disable;
    }
}
