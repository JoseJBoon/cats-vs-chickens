using System;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    [SerializeField] private TechTreeNode techTreeRoot;
    [SerializeField] private RectTransform unitsPanel;
    [SerializeField] private RectTransform buildingsPanel;
    [SerializeField] private RectTransform techsPanel;
    [SerializeField] private TechButton prefabTechButton;

    private readonly Dictionary<TechTreeNode, TechButton> _techTree = new ();
    
    void Awake()
    {
        BuildTechTree(techTreeRoot);
    }

    private void OnDestroy()
    {
        foreach (var entry in _techTree)
        {
            entry.Value.OnTechAcquired -= OnTechAcquired;
            entry.Value.OnTechLost -= OnTechLost;
        }
    }

    private void OnTechAcquired(TechTreeNode node)
    {
        foreach (var techNode in node.Nodes)
        {
            _techTree[techNode].gameObject.SetActive(true);
        }
    }

    private void OnTechLost(TechTreeNode node)
    {
        foreach (var techNode in node.Nodes)
        {
            _techTree[techNode].gameObject.SetActive(false);
        }
    }

    private void BuildTechTree(TechTreeNode node)
    {
        var instance = GameObject.Instantiate(prefabTechButton);
        instance.Initialize(node);
        instance.OnTechAcquired += OnTechAcquired;
        instance.OnTechLost += OnTechLost;
        
        switch (node.Category)
        {
            case Category.Units:
                instance.transform.SetParent(unitsPanel);
                break;
            case Category.Buildings:
                instance.transform.SetParent(buildingsPanel);
                break;
            case Category.Research:
                instance.transform.SetParent(techsPanel);
                break;
            default:
                throw new NotImplementedException($"Missing case for {node.Category}");
        }

        foreach (var childNode in node.Nodes)
        {
            BuildTechTree(childNode);
        }
        _techTree.Add(node, instance);
    }
    
}
