using NUnit.Framework;
using Room;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2Int position;
    private Entity target;
    private EntityData entityData;
    private int ticksElapsedSinceLastAction;

    public EntityType Type => entityData.EntityType;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Vector2Int Position => position;
    public EntityData EntityData => entityData;
    
    public void InitWithData(EntityData entityData, int entityID, Vector2Int position)
    {
        this.entityData = entityData;

        spriteRenderer.sprite = entityData.EntitySprite;
        this.position = position;

        gameObject.name = entityData.name + " " + entityID;
    }

    public void MoveToPosition(Vector2Int newPosition)
    {
        Debug.Log($"{gameObject.name} moved from {position} to {newPosition}");

        RoomGenerator.Instance.GetTile(position).SetOccupied(false);
        position = newPosition;
        
        transform.position = RoomGenerator.Instance.GetTile(position).transform.position;
        RoomGenerator.Instance.GetTile(position).SetOccupied(true);
    }

    public void Tick()
    {
        if(ticksElapsedSinceLastAction >= entityData.Stats.TicksFromDexterity())
        {
            DoMyActions();
            ticksElapsedSinceLastAction = 0;
        }
        else
        {
            ticksElapsedSinceLastAction++;
        }
    }

    public void DoMyActions()
    {
        if(target == null && entityData.Stats.Intelligence > 0)
        {
            target = EntityManager.Instance.GetRandomEntityByType(entityData.Enemies);
            return;
        }
        if(IsInRange(target.Position))
        {
            Action();
        }
        else
        {
            Tuple<Vector2Int, bool> result = RoomGenerator.Instance.GetClosestPositionOnPath(position, target.Position);
            if(result.Item2 == true)
            {
                MoveToPosition(result.Item1);
            }
        }
    }

    public virtual void Action()
    {

    }


    public bool IsInRange(Vector2Int positionToCheck)
    {
        return Vector2Int.Distance(position, positionToCheck) <= entityData.Stats.ActionRange;
    }
}

public enum EntityType
{
    None,
    Predator,
    Prey,
    Food
}
