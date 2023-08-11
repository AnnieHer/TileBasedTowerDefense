using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Towers/CreateNewTower", order = 1)]
public class TowerSO : ScriptableObject
{
    public int baseDamage, baseCost;
    public float baseAS, baseRange, baseAttackInterval, projectileSpeed;
    public string towerName;
    public TargetType targetType;
    public DamageType damageType;
}
