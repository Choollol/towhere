using System.Collections;
using UnityEngine;

public class DoorHandle : InteractableObject
{
    [SerializeField] private Animator doorAnimator;
    private const string OPEN_DOOR_ANIMATION_NAME = "OpenDoorAnimation";
    private const string CLOSE_DOOR_ANIMATION_NAME = "CloseDoorAnimation";

    private const string OPEN_DOOR_SOUND_NAME = "Door Open";
    private const string DOOR_CLOSED_SOUND_NAME = "Door Closed";

    [SerializeField] private bool isLocked = true;

    private void Awake()
    {
        EventMessenger.StartListening(EventKey.EnableDoorHandle, gameObject.Enable);

        gameObject.Disable();
    }
    private void OnDestroy()
    {
        EventMessenger.StopListening(EventKey.EnableDoorHandle, gameObject.Enable);
    }
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
            OpenDoor();
        }
        else
        {
            EventMessenger.TriggerEvent(EventKey.LockedDoorTried, true);
        }
    }
    private void OpenDoor()
    {
        doorAnimator.Play(OPEN_DOOR_ANIMATION_NAME);
        canInteract = false;

        AudioPlayer.PlaySound(OPEN_DOOR_SOUND_NAME, position: transform.position);
    }
    private void CloseDoor()
    {
        doorAnimator.Play(CLOSE_DOOR_ANIMATION_NAME);

        StartCoroutine(WaitToPlayDoorClosedSound());
    }
    private IEnumerator WaitToPlayDoorClosedSound()
    {
        while (doorAnimator.IsCurrentAnimationPlaying()) yield return null;

        AudioPlayer.PlaySound(DOOR_CLOSED_SOUND_NAME, position: transform.position);
    }

    private void Unlock()
    {
        isLocked = false;
    }
}
