using UnityEngine;

[CreateAssetMenu(menuName = "AI/Transition")]
public class Transition : ScriptableObject
{
    public State TargetState;
    public int Priority;
    public Condition Condition;
}
