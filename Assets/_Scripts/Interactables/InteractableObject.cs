using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected bool canInteract = true;
    public virtual bool CanInteract()
    {
        return canInteract;
    }

    public virtual void Interact()
    {
    }
}
