using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _initialState;
    [SerializeField] private List<Transition> _anyTransitions;
    [SerializeField] private List<StateTransitions> _stateTransitions;

    private State _currentState;
    private Dictionary<State, List<Transition>> _transitions = new();

    private void Start()
    {
        foreach (var stateTransition in _stateTransitions)
        {
            _transitions[stateTransition.State] = new List<Transition>(stateTransition.Transitions);
        }

        SetState(_initialState);
    }

    public void SetState(State state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void UpdateStates()
    {
        var transition = GetBestTransition();
        if (transition != null)
        {
            SetState(transition.TargetState);
        }
        _currentState?.Update();
    }

    private Transition GetBestTransition()
    {
        var possibleTransitions = _anyTransitions
            .Concat(_transitions.GetValueOrDefault(_currentState, new List<Transition>()))
            .Where(t => t.Condition.Evaluate())
            .OrderByDescending(t => t.TargetState.Priority) // Priorytet stanów
            .ThenByDescending(t => t.Priority) // Priorytet przejœæ
            .FirstOrDefault();

        return possibleTransitions;
    }
}