using UnityEngine;

public class StateMachineContext
{
    public State currentState;
    public Entity owner;
    public LifeParameters lifeParameters;
    public Entity lastSeenEnemy;
    public Entity lasSeenFood;
    public int idleTurns = 0;

    public void UpdateData()
    {
        lifeParameters.Tick();
    }
}
