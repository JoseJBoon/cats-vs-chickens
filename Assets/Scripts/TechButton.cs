using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TechButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    // Reference to components for Icons (Customization)
    
    private readonly HashSet<Building> _buildings = new ();
    private TechTreeNode _node;
    
    public delegate void OnTechHandler(TechTreeNode node);
    public event OnTechHandler OnTechLost;
    public event OnTechHandler OnTechAcquired;

    private void Awake()
    {
        Building.OnBuildingConstructed += AddBuilding;
        Building.OnBuildingDestroyed += RemoveBuilding;
    }

    private void OnDestroy()
    {
        Building.OnBuildingConstructed -= AddBuilding;
        Building.OnBuildingDestroyed -= RemoveBuilding;
    }

    public void Initialize(TechTreeNode node)
    {
        text.text = node.Name;
        _node = node;
        gameObject.SetActive(node.Unlocked);
    }

    private void AddBuilding(Building building, TechTreeNode node)
    {
        if (_node != node)
        {
            return;
        }
        
        _buildings.Add(building);
        if (_buildings.Count > 0)
        {
            OnTechAcquired?.Invoke(node);
        }
    }

    private void RemoveBuilding(Building building, TechTreeNode node)
    {
        if (_node != node)
        {
            return;
        }
        
        _buildings.Remove(building);
        if (_buildings.Count == 0)
        {
            OnTechLost?.Invoke(node);
        }
    }

    public void Produce()
    {
        foreach (var building in _buildings)
        {
            building.Produce(_node.Prefab);
        }
    }
}
