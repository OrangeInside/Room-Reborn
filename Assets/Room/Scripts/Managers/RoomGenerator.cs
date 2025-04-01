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


        IEnumerator FindingPath(Tile startTile, Tile endTile)
        {
            List<Tile> tilesToCheck = new List<Tile>();

            tilesToCheck.Add(startTile);
            startTile.SetParent(startTile);
            bool foundPath = false;
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
                    else
                    {
                        foundPath = true;
                    }
                }
                //Debug.Log(tilesToCheck.Count);
                yield return new WaitForSeconds(0.1f);


            } while (tilesToCheck.Count > 0);

            ClearColors();

            if (foundPath == false)
            {
                Debug.Log("Could not find path");
                yield break;
            }


            Tile currrentTile = endTile;
            do
            {
                currrentTile.SetColor(Color.cyan);
                currrentTile = currrentTile.Parent;
                yield return new WaitForSeconds(0.2f);

            } while (currrentTile != currrentTile.Parent);
            
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

        private List<Tile> GetAvailableNeighbours(List<Tile> tiles)
        {
            List<Tile> availableNeighbours = new List<Tile>();

            foreach (var tile in tiles)
            {
                if (tile.Parent != null) continue;
                if (tile.TileType == TileType.Wall) continue;
                if (tile.IsOccupied == true) continue;

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

        public Tuple<Vector2Int,bool> GetClosestPositionOnPath(Vector2Int position, Vector2Int targetPosition)
        {
            ClearRooms();

            Tile startTile = GetTile(position.x, position.y);
            Tile targetTile = GetTile(targetPosition.x, targetPosition.y);

            if(startTile == null)
            {
                Debug.Log($"Could not find tile on position: {position}");
                return new Tuple<Vector2Int, bool>(Vector2Int.zero, false);
            }

            if (targetTile == null)
            {
                Debug.Log($"Could not find tile on position: {targetPosition}");
                return new Tuple<Vector2Int, bool>(Vector2Int.zero, false);
            }

            List<Tile> tilesToCheck = new List<Tile>();

            tilesToCheck.Add(startTile);
            startTile.SetParent(startTile);
            bool foundPath = false;
            do
            {
                Tile tileToCheck = tilesToCheck[0];
                tilesToCheck.RemoveAt(0);
                //tileToCheck.SetColor(Color.yellow);

                var neighbours = GetAvailableNeighbours(GetNeighbours(tileToCheck));
                foreach (Tile tile in neighbours)
                {
                    tile.SetParent(tileToCheck);

                    if (targetTile != tile)
                    {
                        //tile.SetColor(Color.grey);
                        tilesToCheck.Add(tile);
                    }
                    else
                    {
                        foundPath = true;
                    }
                }
            } while (tilesToCheck.Count > 0);

            //ClearColors();

            if (foundPath == false)
            {
                Debug.Log("Could not find path");
                return new Tuple<Vector2Int, bool>(Vector2Int.zero, false);
            }

            List<Vector2Int> path = new List<Vector2Int>();
            Tile currrentTile = targetTile;
            path.Add(new Vector2Int(currrentTile.PositionX, currrentTile.PositionY));
            do
            {
                currrentTile = currrentTile.Parent;
                path.Add(new Vector2Int(currrentTile.PositionX, currrentTile.PositionY));

            } while (currrentTile != currrentTile.Parent);

            return new Tuple<Vector2Int, bool>(path[path.Count - 2], true);
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

