using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string entityName;
    [SerializeField] private EntityType entityType;
    [SerializeField] private EntityType enemies;
    [SerializeField] private Sprite entitySprite;
    [SerializeField] private Stats stats;

    public string EntityName => entityName;
    public EntityType EntityType => entityType;
    public EntityType Enemies => enemies;
    public Sprite EntitySprite => entitySprite;
    public Stats Stats => stats;
}

[Serializable]
public struct Stats
{
    [SerializeField] private int strength;
    [SerializeField] private int dexterity;
    [SerializeField] private int constitution;
    [SerializeField] private int intelligence;
    [SerializeField] private int actionRange;

    public int Strength => strength;
    public int Dexterity => dexterity;
    public int Intelligence => intelligence;
    public int Constitution => constitution;
    public int ActionRange => actionRange;

    public int TicksFromDexterity()
    {
        return 10 - dexterity;
    }

}
