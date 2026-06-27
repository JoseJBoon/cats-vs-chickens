using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TechTreeNode", menuName = "Scriptable Objects/TechTree")]
public class TechTreeNode : ScriptableObject
{ 
    [SerializeField] private string _name = "Main";
    [SerializeField] private bool _unlocked = false;
    [SerializeField] private List<TechTreeNode> _nodes = new List<TechTreeNode>();
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _buildTime;
    [SerializeField] private GameObject _prefab;
    [field: SerializeField] public Category Category { get; private set; }
}

public enum Category
{
    None,
    Units,
    Buildings,
    Research,
}
