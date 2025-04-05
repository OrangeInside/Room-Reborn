using NUnit.Framework;
using Room;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    [Header("References from resources")]
    [SerializeField] private TileSet tileSet;
    [SerializeField] private EntitySet entitySet;
    [SerializeField] private SelectableElementUI selectableElementUI;


    [Header("References from prefabs")]
    [SerializeField] private GameObject tilesContainter;
    [SerializeField] private GameObject entitiesContainter;

    private void Awake()
    {
        foreach (var tile in tileSet.Tiles)
        {
            //Instantiate()
        }
    }

    public void GenerateTiles()
    {
        RoomGenerator.Instance.SpawnMap();

        foreach (Tile tile in RoomGenerator.Instance.Tiles)
        {
            //tile.OnTileClick += TileSelected;
        }
    }
 
}

public enum TileSelection
{
    startPoint,
    endPoint
}

public enum ColoringMode
{
    Tiles,
    Entities
}
