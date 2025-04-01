
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Room
{
    public class Tile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TileType tileType;
        [SerializeField] private bool showIndex;

        [SerializeField] private SpriteRenderer tileSprite;
        [SerializeField] private SpriteRenderer entitySprite;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        
        public event Action<Tile> OnTileClick;

        private int positionX, positionY;
        private float weight = 1f;
        private bool isChecked;
        private bool isOccupied = false;

        [HideInInspector]
        public float pathFidingScore;        
        
        private Tile parent;
        private Entity entity;

        public int PositionX => positionX;
        public int PositionY => positionY;
        public Vector2Int Position => new Vector2Int(positionX, positionY);
        public Vector3 WorldPosition => transform.position;
        public bool IsChecked => isChecked;
        public bool IsOccupied => isOccupied;
        public Tile Parent => parent;
        public Button Button => button;
        public TileType TileType => tileType;
        public Entity Entity => entity;
        public float Weight => weight;

        private void Awake()
        {
            text.enabled = showIndex;
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
        public void SetType(TileType tileType)
        {
            this.tileType = tileType;

            switch (tileType) 
            {
                case TileType.Empty:
                    SetColor(Color.white);
                    weight = 1f;
                    break;
                case TileType.Wall:
                    weight = Mathf.Infinity;
                    SetColor(Color.black); break;
                case TileType.Start:
                    weight = 1f;
                    SetColor(Color.red); break;
                case TileType.End:
                    weight = 1f;
                    SetColor(Color.green); break;
                case TileType.Bush:
                    weight = 3f;
                    SetColor(new Color(0.2648748f, 0.490566f, 0.2152011f)); 
                    break;
            }

        }

        public void Clear()
        {
            SetType(tileType);
            parent = null;
        }

        public void ClearColor()
        {
            SetType(tileType);
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

        public void SetChecked(bool value)
        {
            isChecked = value;
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

