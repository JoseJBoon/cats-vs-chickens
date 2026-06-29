using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceChecker : MonoBehaviour
{
    private readonly HashSet<Collider> _overlaps = new(32);

    public bool IsFreeSpace => _overlaps.Count == 0;
    
    private void OnTriggerEnter(Collider other)
    {
        _overlaps.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _overlaps.Remove(other);
    }
}
