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
    bool isRunning = false;

    private void Start()
    {
        context = new StateMachineContext()
        {
            owner = GetComponent<Entity>(),
            lifeParameters = GetComponent<LifeParameters>()
        };
        context.lifeParameters.OnDeath += Stop;
        initialState.Init(context);
        foreach (StateTransitions stateTransition in stateTransitions)
        {
            stateTransition.State.Init(context);
            foreach (Transition transition in stateTransition.Transitions)
            {
                transition.Condition.Init(context);
            }
        }

        foreach (Transition transition in anyTransitions)
        {
            transition.Condition.Init(context);
        }

        foreach (StateTransitions stateTransition in stateTransitions)
        {
            transitions[stateTransition.State] = new List<Transition>(stateTransition.Transitions);
        }

        isRunning = true;
        SetState(initialState);
    }

    public void SetState(State state)
    {
        if (currentState == state) return;
        
        currentState?.Exit();
        currentState = state;
        context.currentState = state;
        currentState.Enter();
    }

    public void UpdateStates()
    {
        if(!isRunning)
        {
            return;
        }
        var transition = GetBestTransition();
        if (transition != null)
        {
            SetState(transition.TargetState);
        }
        context.UpdateData();
        currentState?.Update();
    }

    private Transition GetBestTransition()
    {
        var possibleTransitions = anyTransitions
            .Concat(transitions.GetValueOrDefault(currentState, new List<Transition>()))
            .Where(t => t.Condition.Evaluate())
            .OrderByDescending(t => t.TargetState.Priority) // Priorytet stan�w
            .ThenByDescending(t => t.Priority) // Priorytet przej��
            .FirstOrDefault();

        return possibleTransitions;
    }

    private void Stop()
    {
        isRunning = false;
    }
}