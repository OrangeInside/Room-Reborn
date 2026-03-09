using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/SeesFood")]
public class SeesFoodCondition : Condition
{
    public override bool Evaluate()
    {
        Tile currentTile = RoomGenerator.Instance.GetTile(context.owner.Position);
        List<Tile> neighbours = RoomGenerator.Instance.GetNeighbours(currentTile);

        foreach (var neighbour in neighbours)
        {
            foreach(var entity in neighbour.ResidingEntities)
            {
                if(entity.Type == EntityType.Food)
                {
                    return true;
                }
            }
        }

        return false;
    }

}