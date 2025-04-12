using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private string startDialogueEventName;

    [SerializeField] private DialogueData dialogueData;

    private void OnEnable()
    {
        EventMessenger.StartListening(startDialogueEventName, StartDialogue);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(startDialogueEventName, StartDialogue);
    }

    private void StartDialogue()
    {
        DataMessenger.SetScriptableObject(ScriptableObjectKey.DialogueData, dialogueData);
        EventMessenger.TriggerEvent(EventKey.BeginDialogue);
    }
}
