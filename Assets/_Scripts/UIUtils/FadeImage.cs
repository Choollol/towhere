using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private bool doFadeIn;

    [SerializeField] private string eventToListenFor;

    [SerializeField] private float fadeTime;

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        EventMessenger.StartListening(eventToListenFor, Fade);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(eventToListenFor, Fade);
    }
    private void Fade()
    {
        if (doFadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
    private void FadeIn()
    {
        image.SetAlpha(0);
        StartCoroutine(image.FadeAlpha(1, fadeTime));
        image.enabled = true;
    }
    private void FadeOut()
    {
        image.SetAlpha(1);
        StartCoroutine(image.FadeAlpha(0, fadeTime));
        image.enabled = false;
    }
}
