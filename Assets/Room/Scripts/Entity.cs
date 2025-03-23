using NUnit.Framework;
using Room;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private EntityType targetType;
    [SerializeField] private int actionRange = 1;

    private Vector2Int position;
    private Entity target;
    private Tile tileWithTarget;

    public EntityType Type => type;
    public Sprite Sprite => sprite;
    public Vector2Int Position => position;
    
    public void SetPosition(int x, int y)
    {
        position = new Vector2Int(x, y);
    }

    public void DoYourTurn(List<Tile> tiles)
    {
        if(target == null && targetType != EntityType.None)
        {
            tileWithTarget = tiles.FirstOrDefault(x => x.Entity.Type == targetType);
            target = tileWithTarget.Entity;
            return;
        }
        if(IsInRange(target.Position))
        {
            Action();
        }
        else
        {

        }
    }

    public virtual void Action()
    {

    }

    public void MoveToTile(Tile tileToMove)
    {
        tileToMove.SetEntity(this);
    }

    public bool IsInRange(Vector2Int positionToCheck)
    {
        return Vector2Int.Distance(position, positionToCheck) <= actionRange;
    }

    public Vector2Int GetTargetPosition(List<Tile> tiles)
    {
        Tile tileWithTarget = tiles.FirstOrDefault(x => x.Entity == target);

        return new Vector2Int(tileWithTarget.PositionX, tileWithTarget.PositionY);
    }
}

public enum EntityType
{
    None,
    Predator,
    Prey,
    Food
}
