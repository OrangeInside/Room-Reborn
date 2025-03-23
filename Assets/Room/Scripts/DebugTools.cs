using Room;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;
    
    private TileSelection tileSelectionMode;
    private TileType tilePaintType;

    public void GenerateTiles()
    {
        roomGenerator.SpawnMap();

        foreach (Tile tile in roomGenerator.Tiles)
        {
            tile.OnTileClick += TileSelected;
        }
    }

    public void TilePaintSelected(int tilePaintIndex)
    {
        tilePaintType = (TileType)tilePaintIndex;
    }

    public void TileSelected(Tile tile)
    {
        tile.SetType(tilePaintType);
    }
}

public enum TileSelection
{
    startPoint,
    endPoint
}
