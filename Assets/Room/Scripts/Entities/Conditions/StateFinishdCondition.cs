using UnityEngine;

[CreateAssetMenu(menuName="AI/Conditions/State Finished")]
public class StateFinishedCondition : Condition
{
    public override bool Evaluate()
    {
        return context.currentState is ICompletableState completable 
               && completable.IsFinished();
    }
}

public interface ICompletableState
{
    bool IsFinished();
}