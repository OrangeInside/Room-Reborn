using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
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
            if(
                Instance == null)
            {
                RoomGenerator.Instance = this;
            }
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


        IEnumerator FindingPath(Tile startTile, Tile endTile)
        {
            List<Tile> tilesToCheck = new List<Tile>();

            tilesToCheck.Add(startTile);
            startTile.SetParent(startTile);

            do
            {
                Tile tileToCheck = tilesToCheck[0];
                tilesToCheck.RemoveAt(0);
                tileToCheck.SetColor(Color.yellow);

                var neighbours = GetAvailableNeighbours(GetNeighbours(tileToCheck));
                foreach (Tile tile in neighbours)
                {
                    tile.SetParent(tileToCheck);

                    if (endTile != tile)
                    {
                        tile.SetColor(Color.grey);
                        tilesToCheck.Add(tile);
                    }
                }
                Debug.Log(tilesToCheck.Count);
                yield return new WaitForSeconds(0.1f);


            } while (tilesToCheck.Count > 0);

            ClearColors();

            Tile currrentTile = endTile;
            do
            {
                currrentTile.SetColor(Color.cyan);
                currrentTile = currrentTile.Parent;
                yield return new WaitForSeconds(0.2f);

            } while (currrentTile != currrentTile.Parent);
            
        }

        private Tile GetTile(int positionX, int  positionY)
        {
            return tiles[positionX + (positionY * size)];
        }

        private List<Tile> GetNeighbours(Tile tile)
        {
            List<Tile> neighbours = new List<Tile>();

            if(tile.PositionX - 1 >= 0) neighbours.Add(GetTile(tile.PositionX - 1, tile.PositionY));
            if(tile.PositionX + 1 < size) neighbours.Add(GetTile(tile.PositionX + 1, tile.PositionY));
            if (tile.PositionY - 1 >= 0) neighbours.Add(GetTile(tile.PositionX, tile.PositionY - 1));
            if (tile.PositionY + 1 < size) neighbours.Add(GetTile(tile.PositionX, tile.PositionY + 1));

            return neighbours;
        }

        private List<Tile> GetAvailableNeighbours(List<Tile> tiles)
        {
            List<Tile> availableNeighbours = new List<Tile>();

            foreach (var tile in tiles)
            {
                if (tile.Parent != null) continue;
                if (tile.TileType == TileType.Wall) continue;

                availableNeighbours.Add(tile);
            }

            return availableNeighbours;
        }

        public void FindPathTest()
        {
            ClearRooms();

            Tile startTile = FindTileOfType(TileType.Start);
            Tile endTile = FindTileOfType(TileType.End);

            if(startTile != null &&  endTile != null)
            {
                StartCoroutine(FindingPath(startTile, endTile));
            }
        }

        public void FindPath(Tile startTile, Tile endTile)
        {

        }

        public Tile FindTileOfType(TileType tileTypeToFind)
        {
            return tiles.FirstOrDefault(t => t.TileType == tileTypeToFind);
        }

        public void ClearRooms()
        {
            foreach (Tile tile in tiles)
            {
                tile.Clear();
            }
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

