using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public float damage;
    public float speed;
    public Enemy target;
    public List<Enemy> targets;
    public DamageType damageType;
    public TowerLogic towerLogic;
    public List<AttackModifier> attackModifierList;
    public MapLogic mapLogic;

    public AttackData(
        float attackDamage, 
        float projectileSpeed, 
        Enemy nextTarget, 
        DamageType type, 
        TowerLogic tower, 
        List<AttackModifier> attackModifiers,
        MapLogic map
        ) {
        damage = attackDamage;
        speed = projectileSpeed;
        targets = new List<Enemy>();
        targets.Add(nextTarget);
        damageType = type;
        towerLogic = tower;
        attackModifierList = attackModifiers;
        mapLogic = map;
    }
    public AttackData(
        float attackDamage, 
        float projectileSpeed, 
        Enemy nextTarget, 
        DamageType type, 
        TowerLogic tower, 
        AttackModifier attackModifier,
        MapLogic map
        ) {
        damage = attackDamage;
        speed = projectileSpeed;
        targets = new List<Enemy>();
        targets.Add(nextTarget);
        damageType = type;
        towerLogic = tower;
        attackModifierList = new List<AttackModifier>();
        attackModifierList.Add(attackModifier);
        mapLogic = map;
    }
    public AttackData(
        float attackDamage, 
        float projectileSpeed, 
        List<Enemy> nextTarget, 
        DamageType type, 
        TowerLogic tower, 
        List<AttackModifier> attackModifiers,
        MapLogic map
        ) {
        damage = attackDamage;
        speed = projectileSpeed;
        targets = nextTarget;
        damageType = type;
        towerLogic = tower;
        attackModifierList = attackModifiers;
        mapLogic = map;
    }
}
