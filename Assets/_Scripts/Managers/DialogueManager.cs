using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private const float timeBeforeContinueHintSeconds = 4;
    
    [SerializeField] private GameObject dialoguePrefab;
    private GameObject dialogueInstance;

    private DialogueData currentDialogue = null;

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.BeginDialogue, BeginDialogue);
        
        EventMessenger.StartListening(EventKey.DialogueUnrolled, WaitToShowContinueHint);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.BeginDialogue, BeginDialogue);
        
        EventMessenger.StopListening(EventKey.DialogueUnrolled, WaitToShowContinueHint);
    }
    private void BeginDialogue()
    {
        if (dialogueInstance != null)
        {
            EventMessenger.TriggerEvent(EventKey.FinishUnrollingDialogue);
            EndDialogue();
        }
        
        currentDialogue = (DialogueData)DataMessenger.GetScriptableObject(ScriptableObjectKey.DialogueData);

        currentDialogue.BeginDialogue();

        dialogueInstance = Instantiate(dialoguePrefab);

        EventMessenger.StartListening(EventKey.Continue, Continue);

        Continue();
    }
    private void Continue()
    {
        if (DataMessenger.GetBool(BoolKey.IsDialogueUnrolling))
        {
            //EventMessenger.TriggerEvent(EventKey.FinishUnrollingDialogue);
        }
        else
        {
            StopAllCoroutines();

            DialogueNode node = currentDialogue.GetNextDialogue();
            if (node == null)
            {
                EndDialogue();
                return;
            }

            DataMessenger.SetString(StringKey.DialogueText, node.dialogueText);

            EventMessenger.TriggerEvent(EventKey.UpdateDialogueText);
        }
    }
    private void WaitToShowContinueHint()
    {
        StartCoroutine(HandleWaitToShowContinueHint());
    }
    private IEnumerator HandleWaitToShowContinueHint()
    {
        yield return new WaitForSeconds(timeBeforeContinueHintSeconds);

        EventMessenger.TriggerEvent(EventKey.ShowContinueHint);
    }
    private void EndDialogue()
    {
        Destroy(dialogueInstance);

        EventMessenger.StopListening(EventKey.Continue, Continue);

        dialogueInstance = null;
    }
}
