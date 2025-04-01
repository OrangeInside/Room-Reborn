using NUnit.Framework;
using Room;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    [SerializeField] private List<EntityData> entities;
    
    private TileSelection tileSelectionMode;
    private TileType tilePaintType;
    private EntityData entitySelected;
    private ColoringMode coloringMode;


    public void GenerateTiles()
    {
        RoomGenerator.Instance.SpawnMap();

        foreach (Tile tile in RoomGenerator.Instance.Tiles)
        {
            tile.OnTileClick += TileSelected;
        }
    }

    public void TilePaintSelected(int tilePaintIndex)
    {
        tilePaintType = (TileType)tilePaintIndex;
    }

    public void ColoringModeSelected(int coloringModeIndex)
    {
        coloringMode = (ColoringMode)coloringModeIndex;
    }

    public void EntitySelected(int entityIndex)
    {
        if(entityIndex == 0)
        {
            entitySelected = null;
        }
        else
        {
            entitySelected = entities[entityIndex - 1];
        }
    }

    public void TileSelected(Tile tile)
    {
        switch (coloringMode)
        {
            case ColoringMode.Tiles:
                tile.SetType(tilePaintType);
                break;

            case ColoringMode.Entities:
                if(entitySelected == null)
                {
                    EntityManager.Instance.RemoveEntity(tile.Position);
                }
                else
                {
                    EntityManager.Instance.SpawnEntity(entitySelected, tile.WorldPosition, tile.Position);
                }
                break;

            default:
                break;
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
