using NUnit.Framework;
using Room;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    static public EntityManager Instance;

    List<Entity> entities = new List<Entity>();
    private int entityID;

    public List<Entity> Entities => entities;
     
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        //TimeManager.Instance.OnGameTicked += TickEntitiesInDexterityOrder;
        TimeManager.Instance.OnGameTicked += () => StartCoroutine(TickEntities());
    }

    public void SpawnEntity(Entity entityPrefab, Vector3 worldPosition, Vector2Int tilePosition)
    {
        Entity spawnedEntity = Instantiate(entityPrefab, worldPosition, Quaternion.identity);
        spawnedEntity.Init(entityID, tilePosition);
        entityID++;
        entities.Add(spawnedEntity);
    }

    public void RemoveEntity(Vector2Int position)
    {
        Entity entityToRemove = entities.FirstOrDefault(x => x.Position == position);
        if(entityToRemove != null)
        {
            entities.Remove(entityToRemove);
        }
        else
        {
            Debug.Log($"Could not find entity to remove on position {position}");
        }

        RoomGenerator.Instance.GetTile(position).SetOccupied(false);
    }

    public Entity GetRandomEntityByType(EntityType type)
    {
        return entities.FirstOrDefault(x => x.Type == type);
    }

    private void TickEntitiesInDexterityOrder()
    {
        List<Entity> sortedEntities = entities.OrderBy(x => x.EntityData.Stats.Dexterity).ToList();

        foreach(Entity entity in sortedEntities)
        {
            Debug.Log($"{entity.name} ticked");
            entity.StateMachine.UpdateStates();

        }
    }

    private IEnumerator TickEntities()
    {
        List<Entity> sortedEntities = entities.OrderBy(x => x.EntityData.Stats.Dexterity).ToList();

        foreach (Entity entity in sortedEntities)
        {
            Debug.Log($"{entity.name} ticked");
            entity.StateMachine.UpdateStates();

            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }


}
