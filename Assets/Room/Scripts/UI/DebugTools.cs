using NUnit.Framework;
using Room;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    [Header("References from resources")]
    [SerializeField] private TileSet tileSet;
    [SerializeField] private EntitySet entitySet;
    [SerializeField] private SelectableElementUI selectableElementUI;

    [Header("References from prefab")]
    [SerializeField] private GameObject tilesContainter;
    [SerializeField] private GameObject entitiesContainter;
    [SerializeField] private TMP_InputField xSizeInputField;
    [SerializeField] private TMP_InputField ySizeInputField;

    private Dictionary<GeneralCategory, List<SelectableElementUI>> selectableElements = new Dictionary<GeneralCategory, List<SelectableElementUI>>();
    private SelectableElementUI activeElement;
    private int activeElementIndex = -1;
    private GeneralCategory activeElementCategory;

    private void Awake()
    {
        selectableElements.Add(GeneralCategory.Tile, new List<SelectableElementUI>());
        selectableElements.Add(GeneralCategory.Entity, new List<SelectableElementUI>());

        int index = 0;
        foreach (var tile in tileSet.Tiles)
        {
            var element = Instantiate(selectableElementUI, tilesContainter.GetComponent<RectTransform>());
            selectableElements[GeneralCategory.Tile].Add(element);
            element.Setup(index, GeneralCategory.Tile, tile.texture, Color.white, tile.tileName);
            element.onClick += SelectableElementClicked;
            index++;
        }

        index = 0;
        foreach (var entity in entitySet.Entities)
        {
            var element = Instantiate(selectableElementUI, entitiesContainter.transform);
            selectableElements[GeneralCategory.Entity].Add(element);
            element.Setup(index, GeneralCategory.Entity, entity.Sprite, Color.white, entity.EntityName);
            element.onClick += SelectableElementClicked;
            index++;
        }
    }
    public void GenerateTiles()
    {
        RoomGenerator.Instance.SpawnMap(int.Parse(xSizeInputField.text), int.Parse(ySizeInputField.text));

        foreach (Tile tile in RoomGenerator.Instance.Tiles)
        {
            tile.OnTileClick += MapTileClicked;
            if(activeElement != null && activeElementCategory == GeneralCategory.Tile)
            {
                tile.ApplyData(tileSet.Tiles[activeElementIndex]);
            }
            else
            {
                tile.ApplyData(tileSet.Tiles[0]);
            }
        }
    }

    private void MapTileClicked(Tile tile)
    {
        switch (activeElementCategory)
        {
            case GeneralCategory.Tile:
                tile.ApplyData(tileSet.Tiles[activeElementIndex]);
                break;
            case GeneralCategory.Entity:
                EntityManager.Instance.SpawnEntity(entitySet.Entities[activeElementIndex], tile.WorldPosition, tile.Position);
                break;
            case GeneralCategory.Items:
                break;
        }
    }

    private void SelectableElementClicked(int index, GeneralCategory category)
    {
        activeElement?.Unselect();
        activeElementIndex = index;
        activeElementCategory = category;
        activeElement = selectableElements[category][index];
        activeElement?.Select();
    }
}

public enum GeneralCategory
{
    Tile,
    Entity,
    Items
}
