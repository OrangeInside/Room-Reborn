using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/States/IdleState")]
public class IdleState : State
{
    [SerializeField] private float idlingInPlaceModifier;
    private float chanceForIdlingInPlace;
    private Vector2Int lastPosition;

    public override void Enter() { Debug.Log("Entering Idle State"); }
    public override void Update() 
    {
        if (ShouldMove())
        {
            Tile currentTile = RoomGenerator.Instance.GetTile(context.owner.Position);
            Tile lastTile = RoomGenerator.Instance.GetTile(lastPosition);
            List<Tile> neighbours = RoomGenerator.Instance.GetNeighbours(currentTile);

            List<Tile> legalTiles = new List<Tile>();
            foreach (var neighbour in neighbours)
            {
                if (neighbour.TileData.isWalkable && neighbour != lastTile)
                {
                    legalTiles.Add(neighbour);
                }
            }

            if (legalTiles.Count > 0)
            {
                Tile randomNeighbour = legalTiles[Random.Range(0, legalTiles.Count)];
                context.owner.transform.position = randomNeighbour.WorldPosition;
                context.owner.SetPosition(randomNeighbour.Position);
            }
            else
            {
                context.owner.transform.position = lastTile.WorldPosition;
                context.owner.SetPosition(lastTile.Position);
            }

        }
    }

    private bool ShouldMove()
    {
        float random = Random.Range(0, 100);

        if(random <= chanceForIdlingInPlace)
        {
            chanceForIdlingInPlace = 0;
            return false;
        }
        else
        {
            chanceForIdlingInPlace += idlingInPlaceModifier;
            return true;
        }
    }


    public override void Exit() { Debug.Log("Exiting Idle State"); }
}