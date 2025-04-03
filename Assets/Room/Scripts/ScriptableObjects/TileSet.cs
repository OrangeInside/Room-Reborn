using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "Scriptable Objects/TileSet")]
public class TileSet : ScriptableObject
{
    [SerializeField]
    List<Tile> tiles;

    public List<Tile> Tiles => tiles;
}
