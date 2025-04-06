using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/SearchState")]
public class SearchState : State
{
    public override void Enter() { Debug.Log("Entering Search State"); }
    public override void Update() { Debug.Log("Searching..."); }
    public override void Exit() { Debug.Log("Exiting Search State"); }
}