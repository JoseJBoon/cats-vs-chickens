using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class TechButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image icon;
    [SerializeField] private Image progressBar;
    [SerializeField] private Image highlight;

    public UnityEvent<TechButton, TechTreeNode> onLeftClick = new ();
    public UnityEvent<TechButton, TechTreeNode> onRightClick = new ();
    
    private readonly HashSet<Building> _buildings = new ();
    private TechTreeNode _node;
    
    public delegate void OnTechHandler(TechTreeNode node);
    public event OnTechHandler OnTechLost;
    public event OnTechHandler OnTechAcquired;
    
    /// Is the tech unlocked
    public bool IsUnlocked { get; private set; }
    /// Is the tech constructable from a building
    public bool IsConstructable { get; set; }
    // The set that keeps track the amount of production buildings are present of this tech
    public HashSet<Building> Buildings => _buildings;
    // The set that refers to the required production buildings for producing this tech
    public HashSet<Building> RefToBuildings { get; set; }

    private void Awake()
    {
        progressBar.enabled = false;
        highlight.enabled = false;
        
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

    /// Unlock the tech within the production building
    public void Unlock()
    {
        IsUnlocked = true;
    }

    public void StartProgress(TechTreeNode node)
    {
        progressBar.enabled = true;
        progressBar.fillAmount = 1.0f;
    }

    public void Progress(TechTreeNode node, float progress)
    {
        if (node != _node)
            return;
        
        progressBar.fillAmount = Mathf.Max(0.0f, 1.0f - progress);
        if (progress >= 1.0f)
        {
            highlight.enabled = true;
        }
    }

    public void EndProgress(TechTreeNode node)
    {
        progressBar.enabled = false;
        highlight.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            onLeftClick.Invoke(this, _node);
        else if (eventData.button == PointerEventData.InputButton.Right)
            onRightClick.Invoke(this, _node);
    }
}
