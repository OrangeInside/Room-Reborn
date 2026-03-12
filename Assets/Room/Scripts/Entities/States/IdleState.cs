using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/IdleState")]
public class IdleState : State
{
    public override void Enter()
    {
        context.idleTurns = 0;
        Debug.Log("Entering Idle State");
        
    }
    public override void Update()
    {
        context.idleTurns++;
    }

    public override void Exit() { Debug.Log("Exiting Idle State"); }
}