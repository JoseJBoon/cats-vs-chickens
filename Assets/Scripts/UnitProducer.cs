using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitProducer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float resourceCount = 2000.0f;
    [SerializeField] private float resourceRate = 5.0f;
    
    private readonly List<Unit> _unitProduction = new(10);

    // TODO: The return cash is not accurate
    private void Update()
    {
        float consumption = resourceRate * Time.deltaTime;
        if (resourceCount < consumption)
        {
            return;
        }
        
        var isRunning = false;
        foreach (var unit in _unitProduction)
        {
            if (unit.IsCompleted)
                continue;

            isRunning = true;
            unit.Cost += consumption;
            resourceCount -= consumption;
            if (unit.Cost > unit.TotalCost)
            {
                resourceCount += unit.TotalCost - unit.Cost;
                unit.Cost = unit.TotalCost;
                unit.IsCompleted = true;
            }
        }
        
        enabled = isRunning;
        UpdateText();
    }

    private void UpdateText()
    {
        if (text)
            text.text = $"Coins: {resourceCount,14:N0}";
    }

    public void StartProduction(GameObject prefab, UnitTemplate unitTemplate)
    {
        _unitProduction.Add(new Unit ()
        {
            Cost = 0.0f, TotalCost = unitTemplate.Cost, Prefab = prefab, IsCompleted = false
        });
        enabled = true;
    }

    public void CancelProduction()
    {
        if (_unitProduction.Count == 0)
            return;
        
        resourceCount += _unitProduction[0].Cost;
        _unitProduction.RemoveAt(0);
        if (_unitProduction.Count == 0)
            enabled = false;
        UpdateText();
    }

    public Unit GetFirstCompletedUnit()
    {
        for (var i = 0; i < _unitProduction.Count; ++i)
        {
            if (!_unitProduction[i].IsCompleted)
                continue;

            Unit unit = _unitProduction[i];
            _unitProduction.RemoveAt(i);
            return unit;
        }

        return null;
    }
    
    public class Unit
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
