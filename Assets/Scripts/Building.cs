using UnityEngine;

public delegate void OnProducerHandler(Building producer, TechTreeNode node);

public class Building : MonoBehaviour
{
    public static event OnProducerHandler OnBuildingConstructed;
    public static event OnProducerHandler OnBuildingDestroyed;

    [SerializeField] private TechTreeNode node;

    private void OnEnable()
    {
        OnBuildingConstructed?.Invoke(this, node);
    }

    private void OnDisable()
    {
        OnBuildingDestroyed?.Invoke(this, node);
    }
    
}