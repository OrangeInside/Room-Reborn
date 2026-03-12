using UnityEngine;
using System.Collections.Generic;
using Room;

[CreateAssetMenu(menuName = "AI/States/WanderingState")]
public class WanderingState : State, ICompletableState
{
    [SerializeField] private int minimalDistanceToTravel;
    [SerializeField] private int maximumDistanceToTravel;

    private int distanceToTravel;

    public override void Enter() 
    { 
        Debug.Log("Entering Wandering State");
        distanceToTravel = Random.Range(minimalDistanceToTravel, maximumDistanceToTravel + 1);
    }
    public override void Update()
    {
        Debug.Log($"Wandering. {distanceToTravel}");
        distanceToTravel--; 
        List<Tile> neighbours = RoomGenerator.Instance.GetNeighbours(RoomGenerator.Instance.GetTile(context.owner.Position));
        List<Tile> legalTiles = new List<Tile>();
        foreach (var neighbour in neighbours)
        {
            if (neighbour.TileData.isWalkable)
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
    }
    public override void Exit() { Debug.Log("Exiting Wandering State"); }

    public bool IsFinished()
    {
        return distanceToTravel <= 0;
    }
}