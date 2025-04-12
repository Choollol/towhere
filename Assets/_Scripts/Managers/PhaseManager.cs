using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private int startingPhase;
    private int currentPhase;

    [SerializeField] private Transform playerStartPosition;
    // How many seconds it takes to drag the player back to the starting position
    private const float playerReturnTimeSeconds = 3;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private List<GameObject> phaseEnvironmentObjects;

    private bool isMovingPlayer;

    private void Awake()
    {
        // -1 because NextPhase() is called at beginning of game
        currentPhase = startingPhase - 1;

        foreach (GameObject phaseEnvironmentObject in phaseEnvironmentObjects)
        {
            phaseEnvironmentObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.NextPhase, NextPhase);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.NextPhase, NextPhase);
    }
    private void NextPhase()
    {
        StartCoroutine(ProgressPhaseSequence());
        
    }
    private IEnumerator ProgressPhaseSequence()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, false);

        StartCoroutine(MovePlayerToStart());

        AsyncOperation loadTransition =
            SceneManager.LoadSceneAsync(SceneUtils.SceneName.Transition_Scene.ToString(), LoadSceneMode.Additive);

        while (!loadTransition.isDone) yield return null;

        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning);

        while (isMovingPlayer) yield return null;

        if (currentPhase > 0)
        {
            DisablePhaseEnvironmentObject(currentPhase);
        }
        currentPhase++;
        EnablePhaseEnvironmentObject(currentPhase);

        // Wait one frame so other scripts can detect that screen finished transitioning
        yield return null;

        EventMessenger.TriggerEvent(EventKey.EndScreenTransition);

        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning);


        DataMessenger.SetBool(BoolKey.IsGameActive, true);

        // Trigger first dialogue
        EventMessenger.TriggerEvent($"Phase{currentPhase}Dialogue1");
    }
    private IEnumerator MovePlayerToStart()
    {
        isMovingPlayer = true;

        Vector3 playerPosition = playerTransform.position;

        float timer = 0;
        while (timer < playerReturnTimeSeconds)
        {
            float normalizedTime = timer / playerReturnTimeSeconds;

            playerTransform.position = Vector3.Lerp(playerPosition, playerStartPosition.position, normalizedTime);

            timer += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = playerStartPosition.position;
        
        isMovingPlayer = false;
    }

    private void EnablePhaseEnvironmentObject(int phase)
    {
        phaseEnvironmentObjects[phase - 1].SetActive(true);
    }
    private void DisablePhaseEnvironmentObject(int phase)
    {
        phaseEnvironmentObjects[phase - 1].SetActive(false);
    }
}
