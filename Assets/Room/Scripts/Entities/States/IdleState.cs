using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/IdleState")]
public class IdleState : State
{
    public override void Enter() { Debug.Log("Entering Idle State"); }
    public override void Update() { Debug.Log("Idling..."); }
    public override void Exit() { Debug.Log("Exiting Idle State"); }
}