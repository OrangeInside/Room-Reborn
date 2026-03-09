using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableElementUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image border;
    [SerializeField] TMP_Text label;
    [SerializeField] Button button;
    [SerializeField] Color hoverColor;
    [SerializeField] Color selectedColor;

    public event Action<int, GeneralCategory> onClick;

    private int index;
    private GeneralCategory category;
    bool isSelected = false;

    private void Awake()
    {
        Unselect();
    }

    public void Setup(int index, GeneralCategory category, Sprite sprite, Color color, string label)
    {
        this.index = index;
        this.category = category;
        image.sprite = sprite;
        image.color = color;
        this.label.text = label;
        //button.
    }

    public void Clicked()
    {
        onClick.Invoke(index, category);
    }

    public void Select()
    {
        border.color = selectedColor;
        isSelected = true;
    }

    public void Hover()
    {
        if(!isSelected)
            border.color = hoverColor;
    }

    public void Unhover()
    {
        if (!isSelected)
            border.color = hoverColor;
    }

    public void Unselect()
    {
        border.color = new Color(0,0,0,0);
        isSelected = false;
    }
}
