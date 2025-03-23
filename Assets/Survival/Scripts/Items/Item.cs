using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Texture2D itemTexture;

    [SerializeReference, SubclassPicker]
    public List<ItemEffect> itemEffects = new List<ItemEffect>();

    [SerializeReference, SubclassPicker]
    public List<int> itemEffects2 = new List<int>();
}
