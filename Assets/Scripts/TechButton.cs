using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TechButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image icon;
    
    private readonly HashSet<Building> _buildings = new ();
    private TechTreeNode _node;
    
    public delegate void OnTechHandler(TechTreeNode node);
    public event OnTechHandler OnTechLost;
    public event OnTechHandler OnTechAcquired;
    
    public bool IsUnlocked { get; private set; }
    public bool IsConstructable { get; set; }
    public HashSet<Building> Buildings => _buildings;
    public HashSet<Building> RefToBuildings { get; set; }

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
        icon.sprite = node.Icon;
        text.text = node.Name;
        _node = node;
        gameObject.SetActive(node.Unlocked);
    }

    private void AddBuilding(Building building, TechTreeNode node)
    {
        if (_node != node)
            return;
        
        _buildings.Add(building);
        if (_buildings.Count == 1)
        {
            OnTechAcquired?.Invoke(node);
        }
    }

    private void RemoveBuilding(Building building, TechTreeNode node)
    {
        if (_node != node)
            return;
        
        _buildings.Remove(building);
        if (_buildings.Count == 0)
        {
            OnTechLost?.Invoke(node);
        }
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }

    public void Produce()
    {
        // Start producing
        // Either auto send to production building or keep active
        // 
        
        // TODO: Produce from lowest queue
        foreach (var building in RefToBuildings)
        {
            building.Produce(this, _node.Prefab);
            break;
        }
        // TODO: Lock button; Unlock button on complete;
    }
    
    // state: idle, producing, ready, cancel, 
    
}
