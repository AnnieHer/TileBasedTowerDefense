using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Towers/CreateNewTower", order = 1)]
public class TowerSO : ScriptableObject
{
    public int baseDamage, baseCost;
    public float baseAS, baseRange, baseAttackTime, projectileSpeed;
    public string towerName;
    public TargetType targetType;
    public DamageType damageType;
    public List<AttackModifier> attackModifiers;
    public ProjectileLogic projectile;
}
