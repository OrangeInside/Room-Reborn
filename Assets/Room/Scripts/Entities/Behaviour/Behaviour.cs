using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    private Entity entity;

    // Initialize with the Agent's context
    public virtual void Init(Entity entity)
    {
        this.entity = entity;
    }

    // Core logic (called by States or Agent)
    public abstract void Execute();

    // Cleanup when behaviour is disabled
    public virtual void Reset()
    {
        entity = null;
    }
}