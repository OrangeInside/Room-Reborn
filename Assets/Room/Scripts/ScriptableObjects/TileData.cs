using UnityEngine;

[CreateAssetMenu(menuName = "Grid/TileData")]
public class TileData : ScriptableObject
{
    public string tileName;
    public Sprite texture;
    public float movementCost;
    public bool isWalkable;
}
