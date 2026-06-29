using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProducer : MonoBehaviour
{
    [SerializeField] private float resourceRate = 5.0f;
    
    private readonly List<Unit> _unitProduction = new();

    private void Update()
    {
        for (var i = 0; i < _unitProduction.Count; ++i)
        {
            if (_unitProduction[i].IsCompleted)
                continue;
            
            _unitProduction[i].Cost += resourceRate * Time.deltaTime;
            if (_unitProduction[i].Cost > _unitProduction[i].TotalCost)
            {
                _unitProduction[i].IsCompleted = true;
                // TODO: Return overconsumption of resources
            }
        }
    }

    public void StartProduction(TechTreeNode node)
    {
        _unitProduction.Add(new Unit ()
        {
            Cost = 0.0f, TotalCost = 10.0f, Prefab = null, IsCompleted = false
        });
        enabled = true;
    }

    public void CancelProduction()
    {
        _unitProduction.RemoveAt(0);
        if (_unitProduction.Count == 0)
            enabled = false;
    }
    
    private class Unit
    {
        public float Cost;
        public float TotalCost;
        public GameObject Prefab;
        public bool IsCompleted;
    }
}

// TechButton -> OnClick() -> Start Unit Production -> Consume resources -> A. Spawn
//                                                                          B. Ready to built
//                                                                          C. Apply Tech



// TechButton -> Cancel() -> First item in production
