using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    static public EntityManager Instance;

    [SerializeField] private Entity entityPrefab;

    List<Entity> entities = new List<Entity>();
    private int entityID;
     
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        TimeManager.Instance.OnGameTicked += TickEntitiesInDexterityOrder;
    }

    public void SpawnEntity(EntityData entityData, Vector2Int position)
    {
       Entity spawnedEntity = Instantiate(entityPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
        spawnedEntity.InitWithData(entityData, entityID);
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
            entity.Tick();
        }
    }   


}
