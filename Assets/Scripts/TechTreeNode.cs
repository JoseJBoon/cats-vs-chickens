using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TechTreeNode", menuName = "Scriptable Objects/TechTree")]
public class TechTreeNode : ScriptableObject
{ 
    [field: SerializeField] public string Name { get; private set; } = "TechTreeNode name";
    [field: SerializeField] public bool Unlocked { get; private set; }
    [field: SerializeField] public List<TechTreeNode> Nodes { get; private set; } = new ();
    [field: SerializeField] public List<TechTreeNode> Production { get; private set; } = new();
    [field: SerializeField] public Sprite Icon { get; private set; } = null;
    [field: SerializeField] public float BuildTime { get; private set; } = 1.0f;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Category Category { get; private set; }
}

public enum Category
{
    Units,
    Buildings,
    Research,
}
