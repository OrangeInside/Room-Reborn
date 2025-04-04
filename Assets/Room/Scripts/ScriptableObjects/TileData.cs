using UnityEngine;

[CreateAssetMenu(menuName = "Grid/TileData")]
public class TileData : ScriptableObject
{
    [Header("Visuals")]
    public string tileName;
    public Sprite texture;
    public Color color;

    [Header("Properties")]
    public float movementCost;
    public bool isWalkable;

}
