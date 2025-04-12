using UnityEngine;

public class PlaySoundEventListener : MonoBehaviour
{
    [SerializeField] private string eventToListenFor;

    [SerializeField] private string soundToPlay;

    [SerializeField] private bool doPlaySoundAtPosition;

    private void OnEnable()
    {
        EventMessenger.StartListening(eventToListenFor, PlaySound);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(eventToListenFor, PlaySound);
    }
    private void PlaySound()
    {
        if (doPlaySoundAtPosition)
        {
            AudioPlayer.PlaySound(soundToPlay, position: transform.position);
        }
        else
        {
            AudioPlayer.PlaySound(soundToPlay);
        }
    }
}
