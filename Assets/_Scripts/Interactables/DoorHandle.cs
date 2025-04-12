using UnityEngine;

public class DoorHandle : InteractableObject
{
    [SerializeField] private Animator doorAnimator;
    private const string OPEN_DOOR_ANIMATION_NAME = "OpenDoorAnimation";

    private bool isLocked = true;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.UnlockDoor, Unlock);
        
        EventMessenger.StartListening(EventKey.CloseDoor, CloseDoor);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.UnlockDoor, Unlock);
        
        EventMessenger.StopListening(EventKey.CloseDoor, CloseDoor);
    }

    public override void Interact()
    {
        base.Interact();

        if (!isLocked)
        {
            doorAnimator.Play(OPEN_DOOR_ANIMATION_NAME);
        }
        else
        {
            EventMessenger.TriggerEvent(EventKey.DoorIsLocked, true);
        }
    }
    private void CloseDoor()
    {

    }

    private void Unlock()
    {
        isLocked = false;
    }
}
