using System.Collections;
using TMPro;
using UnityEngine;

public static class TextMeshProUtils
{
    public static void SetAlpha(this TextMeshProUGUI text, float opacity)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
    public static IEnumerator FadeAlpha(this TextMeshProUGUI text, float targetOpacity, float duration)
    {
        if (text.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = text.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        text.SetAlpha(targetOpacity);
        yield break;
    }
}