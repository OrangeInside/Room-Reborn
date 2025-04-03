using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Room
{
    public class RoomGenerator : MonoBehaviour
    {
        public static RoomGenerator Instance;

        [SerializeField] private int size = 5;
        [SerializeField] private Tile tilePrefab;
        [SerializeField] Transform mapContainer;

        private List<Tile> tiles;
        public List<Tile> Tiles => tiles;


        private void Awake()
        {
            if(Instance == null)
            {
                RoomGenerator.Instance = this;
            }

            //SpawnMap();
        }

        public void SpawnMap()
        {
            tiles = new List<Tile>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Tile spawnedTile = Instantiate(tilePrefab, mapContainer);
                    spawnedTile.transform.localPosition = new Vector3(j, i, 0);
                    spawnedTile.SetPosition(j, i);
                    tiles.Add(spawnedTile);
                }
            }
        }

        public Tile GetTile(int positionX, int  positionY)
        {
            return tiles[positionX + (positionY * size)];
        }

        public Tile GetTile(Vector2Int position)
        {
            return tiles[position.x + (position.y * size)];
        }

        public List<Tile> GetNeighbours(Tile tile)
        {
            List<Tile> neighbours = new List<Tile>();

            if(tile.PositionX - 1 >= 0) neighbours.Add(GetTile(tile.PositionX - 1, tile.PositionY));
            if(tile.PositionX + 1 < size) neighbours.Add(GetTile(tile.PositionX + 1, tile.PositionY));
            if (tile.PositionY - 1 >= 0) neighbours.Add(GetTile(tile.PositionX, tile.PositionY - 1));
            if (tile.PositionY + 1 < size) neighbours.Add(GetTile(tile.PositionX, tile.PositionY + 1));

            return neighbours;
        }
  
        public Tile FindTileOfType(string tileTypeToFind)
        {
            return tiles.FirstOrDefault(t => t.TileData.tileName == tileTypeToFind);
        }

        public void ClearRooms()
        {
            foreach (Tile tile in tiles)
            {
                tile.Clear();
            }
        }

        public Vector2Int GetPositionFurthestFromEntityType(Vector2Int currentPosition, EntityType entityType)
        {
            List<float> distancesToEntities = new List<float>();
            List<Entity> entities = EntityManager.Instance.Entities;

            foreach (var tile in Tiles)
            {
                float distanceSum = 0f;
                foreach (var entity in entities)
                {
                    if(entity.EntityData.EntityType == entityType)
                    {
                        distanceSum += Vector2.Distance(tile.Position, entity.Position);
                    }
                }
                tile.SetText(distanceSum.ToString());
                distancesToEntities.Add(distanceSum);
            }

            int furthestIndex = distancesToEntities.IndexOf(distancesToEntities.Max());
            return Tiles[furthestIndex].Position;
        }

        public void ClearColors()
        {
            foreach (Tile tile in tiles)
            {
                tile.ClearColor();
            }
        }

    }
}

