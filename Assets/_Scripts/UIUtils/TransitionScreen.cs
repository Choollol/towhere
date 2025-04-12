using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour
{
    private const float WAIT_TIME_SECONDS = 1;

    private Animator animator;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.EndScreenTransition, EndTransition);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.EndScreenTransition, EndTransition);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        BeginTransition();
    }

    // Stop transition if it is currently active
    private void StopTransition()
    {
        StopAllCoroutines();

        // Check if the transition is active
        if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning))
        {
            // Screen is no longer transitioning, update DataMessenger accordingly
            DataMessenger.SetBool(BoolKey.IsScreenTransitioning, false);
        }
    }

    private void BeginTransition()
    {
        StopTransition();

        StartCoroutine(BeginAndWaitForAnimation("TransitionScreenBeginAnimation", true));
    }
    private void EndTransition()
    {
        StopTransition();

        StartCoroutine(BeginAndWaitForAnimation("TransitionScreenEndAnimation", false));
    }
    private IEnumerator BeginAndWaitForAnimation(string animationName, bool isTransitioningIn)
    {
        DataMessenger.SetBool(BoolKey.IsScreenTransitioning, true);

        animator.Play(animationName);

        yield return null;

        // Wait for animation to stop playing
        while (animator.IsCurrentAnimationPlaying()) yield return null;

        if (isTransitioningIn)
        {
            yield return new WaitForSeconds(WAIT_TIME_SECONDS);
        }
        else
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        DataMessenger.SetBool(BoolKey.IsScreenTransitioning, false);
    }
}