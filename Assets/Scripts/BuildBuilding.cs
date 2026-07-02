using UnityEngine;

[RequireComponent(typeof(UnitProducer))]
public class BuildBuilding : MonoBehaviour
{
    public delegate void OnBuildHandler(TechTreeNode node);
    public delegate void OnBuildProgressHandler(TechTreeNode node, float progress);
    
    [SerializeField] private Placement placement;
    
    private UnitProducer _unitProducer;
    private TechButton _orderFrom;
    private TechTreeNode _node;
    private UnitProducer.Unit _currentUnit;
    
    public event OnBuildHandler OnBuildStart;
    public event OnBuildHandler OnBuildComplete;
    public event OnBuildProgressHandler OnBuildProgress;
    
    private void Awake()
    {
        placement.OnBuildingPlaced += OnBuildCompleteAction;
        _unitProducer = GetComponent<UnitProducer>();
    }

    private void Update()
    {
        if (!_orderFrom)
            return;
        OnBuildProgress?.Invoke(_node, _unitProducer.CurrentUnitProgress());
    }

    public void OnBuildAction(TechButton button, TechTreeNode node)
    {
        if (_orderFrom == null)
        {
            _unitProducer.StartProduction(node.Prefab, node.UnitStat);
            _orderFrom = button;
            _node = node;
            OnBuildStart?.Invoke(node);
            return;
        }

        if (_orderFrom != button)
            return;

        if (_currentUnit != null)
        {
            placement.AssignBuilding(_currentUnit.Prefab.transform);
            return;
        }
        
        _currentUnit = _unitProducer.GetFirstCompletedUnit();
        if (_currentUnit == null)
            return;
        
        placement.AssignBuilding(_currentUnit.Prefab.transform);
    }

    public void OnBuildCompleteAction()
    {
        _orderFrom = null;
        _currentUnit = null;
        
        OnBuildComplete?.Invoke(_node);
        _node = null;
    }

    public void OnCancelBuildAction(TechButton button, TechTreeNode node)
    {
        if (button != _orderFrom)
            return;

        if (_currentUnit != null)
        {
            _unitProducer.RefundUnit(_currentUnit);
            _currentUnit = null;
        }
        else
        {
            _unitProducer.CancelProduction();
        }
        OnBuildComplete?.Invoke(node);
        _node = null;
        _orderFrom = null;
    }
}
