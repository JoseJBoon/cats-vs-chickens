using System;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "Scriptable Objects/UnitTemplate")]
public class UnitTemplate : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; } = "Unknown";
    [field: SerializeField] public float HitPoints { get; private set; } = 100.0f;	// Attacks per second
    [field: SerializeField] public float AttackSpeed { get; private set; } = 1.0f;
    [field: SerializeField] public float Damage { get; private set; } = 1.0f;
    [field: SerializeField] public Vector2 DamageOverTime { get; private set; } = new Vector2(1.0f, 1.0f);	// { Damage, duration }
    [field: SerializeField] public float Range { get; private set; } = 0.0f;
    [field: SerializeField] public float AbilityRange { get; private set; } = 0.0f;
    [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(1, 1);
    [field: SerializeField] public float MovementSpeed { get; private set; } = 0.0f;
    [field: SerializeField] public float Acceleration { get; private set; } = 0.0f;
    [field: SerializeField] public float Cost { get; private set; } = 1.0f;
}
