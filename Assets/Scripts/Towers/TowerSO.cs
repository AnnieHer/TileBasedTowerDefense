using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Towers/CreateNewTower", order = 1)]
public class TowerSO : ScriptableObject
{
    public int baseDamage, baseCost, targetCount;
    public float baseAS, baseRange, baseAttackInterval;
    public string towerName;
    public TargetType targetType;
    public enum TargetType {
        nonTarget,
        target,
        point,
        direction,
    }
    public DamageType damageType;
    public enum DamageType {
        physical,
        magical,
        pure,
        none
    }
}
