using NUnit.Framework;
using Room;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("References from prefab")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private StateMachine stateMachine;

    private EntityData entityData;
    private Vector2Int position;
    private int ticksElapsedSinceLastAction;

    public EntityData EntityData => entityData;
    public Vector2Int Position => position;
    public EntityType Type => entityData.EntityType;
    public StateMachine StateMachine => stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();
    }

    public void Init(int entityID, Vector2Int position)
    {
        this.position = position;

        gameObject.name = entityData.name + " " + entityID;
    }
}

public enum EntityType
{
    None,
    Predator,
    Prey,
    Food
}
