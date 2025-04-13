using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private string startDialogueEventName;

    [SerializeField] private DialogueData dialogueData;

    [SerializeField] private int maxDialogueTriggerCount = -1;

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
        if (maxDialogueTriggerCount == 0)
        {
            return;
        }

        DataMessenger.SetScriptableObject(ScriptableObjectKey.DialogueData, dialogueData);
        EventMessenger.TriggerEvent(EventKey.BeginDialogue);

        maxDialogueTriggerCount--;
    }
}
