using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class ImageUtils
{
    public static void SetAlpha(this Image image, float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }

    public static IEnumerator FadeAlpha(this Image image, float targetOpacity, float duration)
    {
        if (image.color.a == targetOpacity) yield break;

        float currentTime = 0;
        float start = image.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            image.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        image.SetAlpha(targetOpacity);
    }
}