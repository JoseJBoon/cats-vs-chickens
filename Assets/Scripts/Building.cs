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

    public void Produce(TechButton from, GameObject techPrefab)
    {
        if (!from || !techPrefab) // TODO: Should never be NULL
        {
            Debug.Log($"{name} WANTS TO BUILD!!!!");
            return;
        }
        Debug.Log($"{from.name} wants to produce {techPrefab.name}");
        
        // Spawn Unit; Unlock Tech; Start Building placement
    }
}