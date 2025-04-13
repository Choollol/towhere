using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineEventListener : MonoBehaviour
{
    [SerializeField] private string eventToListenFor;

    private PlayableDirector timeline;

    [SerializeField] private bool doReturnToGame;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(eventToListenFor, PlayTimeline);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(eventToListenFor, PlayTimeline);
    }
    private void PlayTimeline()
    {
        timeline.Play();

        DataMessenger.SetBool(BoolKey.IsGameActive, false);

        if (doReturnToGame)
        {
            StartCoroutine(WaitForTimelineEnd());
        }
    }
    private IEnumerator WaitForTimelineEnd()
    {
        while (timeline.state == PlayState.Playing) yield return null;

        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }
}
