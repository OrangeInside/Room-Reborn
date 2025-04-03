
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Room
{
    public class Tile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpriteRenderer tileSprite;
        [SerializeField] private SpriteRenderer borderSprite;
        [SerializeField] private SpriteRenderer entitySprite;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        
        public event Action<Tile> OnTileClick;
        
        private TileData tileData;
        private int positionX, positionY;
        private float weight = 1f;
        private bool isOccupied = false;

        [HideInInspector]
        public float pathFidingScore;        
        
        private Tile parent;
        private Entity entity;

        public TileData TileData => tileData;
        public int PositionX => positionX;
        public int PositionY => positionY;
        public Vector2Int Position => new Vector2Int(positionX, positionY);
        public Vector3 WorldPosition => transform.position;
        public bool IsOccupied => isOccupied;
        public Tile Parent => parent;
        public Entity Entity => entity;
        public float Weight => weight;

        private void Awake()
        {
            text.enabled = false;
        }

        public void SetColor(Color color)
        {
            tileSprite.color = color;
        }

        public void SetParent(Tile tile)
        {
            parent = tile;
        }
        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public void Clear()
        {
            //SetData(tileType);
            parent = null;
        }

        public void ClearColor()
        {
            //SetType(tileType);
        }

        public void Clicked()
        {
            OnTileClick.Invoke(this);
        }

        public void SetPosition(int x, int y)
        {
            positionX = x;
            positionY = y;

            //text.text = (x + (y * 10)).ToString();
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetOccupied(bool value)
        {
            isOccupied = value;
        }
    }

    public enum TileType
    {
        Empty,
        Wall,
        Start,
        End,
        Bush
    }
}

