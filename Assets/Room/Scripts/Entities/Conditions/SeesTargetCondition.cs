using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/SeesTarget")]
public class SeesTargetCondition : Condition
{
    public Transform agent;
    public Transform target;
    public float visionRange = 10f;

    public override bool Evaluate()
    {
        return Vector3.Distance(agent.position, target.position) <= visionRange;
    }

}