using NUnit.Framework;
using Room;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitySet", menuName = "Scriptable Objects/EntitySet")]
public class EntitySet : ScriptableObject
{
    [SerializeField]
    List<Entity> entities;

    public List<Entity> Entities => entities;
}
