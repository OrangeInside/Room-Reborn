using NUnit.Framework;
using Room;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void FindPathTest()
    {
        RoomGenerator.Instance.ClearRooms();

        Tile startTile = RoomGenerator.Instance.FindTileOfType("TestStart");
        Tile endTile = RoomGenerator.Instance.FindTileOfType("TestEnd");

        List<Tile> path = new List<Tile>();

        if (startTile != null && endTile != null)
        {
            path = FindShortestPath(startTile.Position, endTile.Position);
        }

        foreach (var tile in path)
        {
            tile.SetColor(Color.cyan);
        }
    }

    public void FindPathTestVisualisation()
    {
        RoomGenerator.Instance.ClearRooms();

        Tile startTile = RoomGenerator.Instance.FindTileOfType("TestStart");
        Tile endTile = RoomGenerator.Instance.FindTileOfType("TestEnd");

        List<Tile> path = new List<Tile>();

        if (startTile != null && endTile != null)
        {
            StartCoroutine(FindShortestPathVisualisation(startTile.Position, endTile.Position));
        }

        foreach (var tile in path)
        {
            tile.SetColor(Color.cyan);
        }
    }

    public IEnumerator FindShortestPathVisualisation(Vector2Int start, Vector2Int target)
    {
        Tile startTile = RoomGenerator.Instance.GetTile(start.x, start.y);
        Tile targetTile = RoomGenerator.Instance.GetTile(target.x, target.y);

        Dictionary<Tile, float> gScore = new Dictionary<Tile, float>();
        Dictionary<Tile, float> fScore = new Dictionary<Tile, float>();

        foreach (Tile tile in RoomGenerator.Instance.Tiles)
        {
            gScore.Add(tile, Mathf.Infinity);
            fScore.Add(tile, Mathf.Infinity);
        }

        gScore[startTile] = 0;
        fScore[startTile] = GetDistanceToTarget(startTile, startTile);

        List<Tile> closedSet = new List<Tile>();
        List<Tile> openSet = new List<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            Tile current = GetTileWithLowestFScore(ref openSet, ref fScore);
            current.SetColor(Color.yellow);
            if (current == targetTile)
            {
                //return null;
            }

            openSet.Remove(current);

            List<Tile> neighbours = RoomGenerator.Instance.GetNeighbours(current);

            foreach (Tile neighbour in neighbours)
            {
                float tentativeGScore = gScore[current] + neighbour.Weight;
                current.SetColor(Color.grey);
                if (tentativeGScore < gScore[neighbour])
                {
                    cameFrom.Add(neighbour, current);
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = tentativeGScore + GetDistanceToTarget(neighbour, targetTile);
                    neighbour.SetText(tentativeGScore.ToString() + " , " + Math.Round(GetDistanceToTarget(neighbour, targetTile))); ;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        Debug.Log("Path finder could not find the path");
        yield break;
    }

    public List<Tile> FindShortestPath(Vector2Int start, Vector2Int target)
    {
        Tile startTile = RoomGenerator.Instance.GetTile(start.x, start.y);
        Tile targetTile = RoomGenerator.Instance.GetTile(target.x, target.y);

        Dictionary<Tile, float> gScore = new Dictionary<Tile, float>();
        Dictionary<Tile, float> fScore = new Dictionary<Tile, float>();

        foreach (Tile tile in RoomGenerator.Instance.Tiles)
        {
            gScore.Add(tile, Mathf.Infinity);
            fScore.Add(tile, Mathf.Infinity);
        }

        gScore[startTile] = 0;
        fScore[startTile] = GetDistanceToTarget(startTile, startTile);
        
        List<Tile> closedSet = new List<Tile>();
        List<Tile> openSet = new List<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

        openSet.Add(startTile);

        while(openSet.Count > 0)
        {
            Tile current = GetTileWithLowestFScore(ref openSet, ref fScore);
            if (current == targetTile)
            {
                return ReconstructPath(ref cameFrom, current);
            }

            openSet.Remove(current);

            List<Tile> neighbours = RoomGenerator.Instance.GetNeighbours(current);

            foreach (Tile neighbour in neighbours)
            {
                float tentativeGScore = gScore[current] + neighbour.Weight;

                if(tentativeGScore < gScore[neighbour])
                {
                    cameFrom.Add(neighbour, current);
                    gScore[neighbour] = tentativeGScore;
                    fScore[neighbour] = tentativeGScore + GetDistanceToTarget(neighbour, targetTile);

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        Debug.Log("Path finder could not find the path");
        return null;
    }

    private Tile GetTileWithLowestFScore(ref List<Tile> openSet, ref Dictionary<Tile, float> fScore)
    {
        float minimum = Mathf.Infinity;
        Tile tileWithLowestFScore = null;

        foreach (Tile tile in openSet)
        {
            if (fScore[tile] < minimum)
            {
                minimum = fScore[tile];
                tileWithLowestFScore = tile;
            }
        }

        return tileWithLowestFScore;
    }

    private List<Tile> ReconstructPath(ref Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        List<Tile> totalPath = new List<Tile> ();
        totalPath.Add(current);

        while(cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath = totalPath.Prepend(current).ToList();
        }

        return totalPath;
    }

    private float GetDistanceToTarget(Tile A, Tile B)
    {
        return Vector2.Distance(A.Position, B.Position);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
