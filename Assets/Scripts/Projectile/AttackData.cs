using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public float damage;
    public float speed;
    public Enemy target;
    public List<AttackModifier> attackModifierList = new List<AttackModifier>();

    public AttackData(float attackDamage, float projectileSpeed, Enemy nextTarget) {
        damage = attackDamage;
        speed = projectileSpeed;
        target = nextTarget;
    }
    public AttackData() {
        damage = 0;
        speed = 1;
        target = EnemyManager.Instance.GetRandomEnemy();
    }
}
