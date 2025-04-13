using TMPro;
using UnityEngine;

public class ContinueHintText : MonoBehaviour
{
    private const float fadeTime = 2;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        HideText();
    }

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.ShowContinueHint, ShowText);

        EventMessenger.StartListening(EventKey.Continue, HideText);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.ShowContinueHint, ShowText);

        EventMessenger.StopListening(EventKey.Continue, HideText);
    }
    private void ShowText()
    {
        text.enabled = true;

        text.SetAlpha(0);
        StartCoroutine(text.FadeAlpha(1, fadeTime));
    }
    private void HideText()
    {
        text.enabled = false;

        StopAllCoroutines();
    }
}
