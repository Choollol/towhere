using System.Collections.Generic;
using UnityEngine;

public class PhaseDependentActivation : MonoBehaviour
{
    [SerializeField] private List<int> activePhases;
    private void Awake()
    {
        EventMessenger.StartListening(EventKey.PhaseProgressed, PhaseProgressed);
    }
    private void OnDestroy()
    {
        EventMessenger.StopListening(EventKey.PhaseProgressed, PhaseProgressed);
    }
    private void Start()
    {
        PhaseProgressed();
    }
    private void PhaseProgressed()
    {
        gameObject.SetActive(activePhases.Contains(DataMessenger.GetInt(IntKey.CurrentPhase)));
    }
}
