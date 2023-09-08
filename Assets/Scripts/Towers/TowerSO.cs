using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Towers/CreateNewTower", order = 1)]
public class TowerSO : ScriptableObject
{
    public int baseCost;
    public Stats baseStats;
    public float baseAttackTime, projectileSpeed;
    public string towerName;
    public Rarity towerRarity;
    public TargetType targetType;
    public DamageType damageType;
    public List<AttackModifier> attackModifiers;
    public List<UpgradeUnit> upgrades;
    public ProjectileLogic projectile;
    public GameObject prefab;
}
