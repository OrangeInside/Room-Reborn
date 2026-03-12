using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/BoredCondition")]
public class BoredCondition : Condition
{
    [SerializeField] private int minimumNumberOfIdleTurns;
    public override bool Evaluate()
    {
        return context.idleTurns >= minimumNumberOfIdleTurns;
    }

}