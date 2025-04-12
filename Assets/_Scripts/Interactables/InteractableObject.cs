using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private bool canInteract = true;
    public bool CanInteract()
    {
        return canInteract;
    }

    public virtual void Interact()
    {
    }
}
