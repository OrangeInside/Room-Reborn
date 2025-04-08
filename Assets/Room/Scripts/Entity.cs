using NUnit.Framework;
using Room;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class Entity : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string entityName;
    [SerializeField] private EntityType entityType;
    [SerializeField] private EntityType mainEnemy;
    [SerializeField] private Stats stats;

    [Header("References from prefab")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private StateMachine stateMachine;
    private Vector2Int position;
    private int ticksElapsedSinceLastAction;

    public string EntityName => entityName;
    public EntityType Type => entityType;
    public Stats Stats => stats;
    public Sprite Sprite => spriteRenderer.sprite;
    public Vector2Int Position => position;
    public StateMachine StateMachine => stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    public void Init(int entityID, Vector2Int position)
    {
        this.position = position;

        gameObject.name = entityName + " " + entityID;
    }

    public void SetPosition(Vector2Int position)
    {
        this.position = position;
    }
}

public enum EntityType
{
    None,
    Predator,
    Prey,
    Food
}
