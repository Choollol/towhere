using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerer : MonoBehaviour
{
    [SerializeField] private List<string> awakeEventsToTrigger;
    [SerializeField] private List<string> startEventsToTrigger;
    private void Awake()
    {
        foreach (string ev in awakeEventsToTrigger)
        {
            EventMessenger.TriggerEvent(ev);
        }
    }
    private void Start()
    {
        foreach (string ev in startEventsToTrigger)
        {
            EventMessenger.TriggerEvent(ev);
        }
    }
    public void TriggerEvent(string eventToTrigger)
    {
        EventMessenger.TriggerEvent(eventToTrigger);
    }
}