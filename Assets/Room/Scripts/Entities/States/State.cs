using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(menuName = "AI/State")]
public abstract class State : ScriptableObject
{
    public int Priority { get; protected set; } 
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
