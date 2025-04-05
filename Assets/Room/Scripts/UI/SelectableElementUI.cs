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

    public event Action<int, GeneralCategory> onClick;

    private int index;
    private GeneralCategory category;

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
    }

    public void Clicked()
    {
        onClick.Invoke(index, category);
    }

    public void Select()
    {
        border.enabled = true;
    }

    public void Unselect()
    {
        border.enabled = false;
    }
}
