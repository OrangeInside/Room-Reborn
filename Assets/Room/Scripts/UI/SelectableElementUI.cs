using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableElementUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text label;
    [SerializeField] Button button;

    public event Action<int> onClick;

    private int index;

    public void Setup(int index, Sprite sprite, Color color, string label)
    {
        this.index = index;
        image.sprite = sprite;
        image.color = color;
        this.label.text = label;
    }

    public void Clicked()
    {
        onClick.Invoke(index);
    }
}
