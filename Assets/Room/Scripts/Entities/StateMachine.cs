using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State initialState;
    [SerializeField] private List<Transition> anyTransitions;
    [SerializeField] private List<StateTransitions> stateTransitions;

    private State currentState;
    private StateMachineContext context;
    private Dictionary<State, List<Transition>> transitions = new();

    private void Start()
    {
        context = new StateMachineContext()
        {
            owner = GetComponent<Entity>()
        };

        initialState.Init(context);
        foreach (var stateTransition in stateTransitions)
        {
            stateTransition.State.Init(context);
            foreach (var transition in stateTransition.Transitions)
            {
                transition.Condition.Init(context);
            }
        }

        foreach (var transition in anyTransitions)
        {
            transition.Condition.Init(context);
        }


        foreach (var stateTransition in stateTransitions)
        {
            transitions[stateTransition.State] = new List<Transition>(stateTransition.Transitions);
        }

        SetState(initialState);
    }

    public void SetState(State state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void UpdateStates()
    {
        var transition = GetBestTransition();
        if (transition != null)
        {
            SetState(transition.TargetState);
        }
        currentState?.Update();
    }

    private Transition GetBestTransition()
    {
        var possibleTransitions = anyTransitions
            .Concat(transitions.GetValueOrDefault(currentState, new List<Transition>()))
            .Where(t => t.Condition.Evaluate())
            .OrderByDescending(t => t.TargetState.Priority) // Priorytet stanów
            .ThenByDescending(t => t.Priority) // Priorytet przejœæ
            .FirstOrDefault();

        return possibleTransitions;
    }
}