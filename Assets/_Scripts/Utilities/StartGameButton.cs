using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GameObject startGameUICanvasObject;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(StartGame);

        EventMessenger.TriggerEvent(EventKey.ShowCursor);
    }

    private void StartGame()
    {
        EventMessenger.TriggerEvent(EventKey.NextPhase);

        EventMessenger.TriggerEvent(EventKey.HideCursor);

        StartCoroutine(WaitToDisable());
    }
    private IEnumerator WaitToDisable()
    {
        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning, true);
        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning);

        button.onClick.RemoveAllListeners();

        startGameUICanvasObject.SetActive(false);
    }
}
