using UnityEngine;

public abstract class Condition : ScriptableObject
{
    protected StateMachineContext context;
    public virtual void Init(StateMachineContext context)
    { 
        this.context = context; 
    }
    public abstract bool Evaluate();
}