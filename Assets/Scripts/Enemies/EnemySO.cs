using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy SO", menuName = "Enemies/New Enemy")]
public class EnemySO : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float barrierHealth;
    public DamageType barrierType;
}
