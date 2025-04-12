using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    private const float interactDistance = 3f;

    private const string INTERACT_BUTTON_NAME = "Interact";

    [SerializeField] private Camera playerCamera;

    private bool canInteract = true;

    [SerializeField] private LayerMask interactLayers;

    private void Update()
    {
        if (canInteract &&
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit,
                interactDistance, interactLayers)
            && hit.transform.GetComponent(typeof(IInteractable)) is IInteractable interactable && interactable.CanInteract())
        {
            if (Input.GetButtonDown(INTERACT_BUTTON_NAME))
            {
                interactable.Interact();
            }
            if (!DataMessenger.GetBool(BoolKey.IsInteractIndicatorActive))
            {
                EventMessenger.TriggerEvent(EventKey.EnableInteractIndicator);
            }
        }
        else
        {
            if (DataMessenger.GetBool(BoolKey.IsInteractIndicatorActive))
            {
                EventMessenger.TriggerEvent(EventKey.DisableInteractIndicator);
            }
        }
    }
    private void DisableInteract()
    {
        canInteract = false;
    }
    private void EnableInteract()
    {
        canInteract = true;
    }
}