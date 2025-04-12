using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterEventTriggerer : MonoBehaviour
{
    [SerializeField] private List<string> eventsToTrigger; // Names of events to trigger

    // Optional list of objects that this script will check for.
    // If the list is not empty, only trigger-colliding with the objects in the list will trigger events.
    [SerializeField] private List<GameObject> triggerObjects;

    // If the list is not empty, only trigger-colliding with the objects that have tags in the list will trigger events.
    [SerializeField] private List<string> triggerTags;

    [SerializeField] private int triggerCount = 1; // Number of times this script can trigger events. -1 for infinite

    private void OnTriggerEnter(Collider other)
    {
        bool matchesTriggerObject = triggerObjects.Count == 0 || triggerObjects.Contains(other.gameObject);
        bool matchesTag = triggerTags.Count == 0 || triggerTags.Contains(other.tag);
        bool hasTriggersLeft = triggerCount > 0;

        // Check if objects that can trigger events when colliding with this object are restricted
        if (matchesTriggerObject && matchesTag && hasTriggersLeft && DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            // Trigger events and decrement triggerCount
            foreach (string ev in eventsToTrigger)
            {
                EventMessenger.TriggerEvent(ev);
            }
            triggerCount--;
        }
    }
}