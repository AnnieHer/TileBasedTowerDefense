using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackModifier", menuName = "Attack Modifiers/ChainLightning")]
public class ChainLightning : AttackModifier
{
    public int jumps;
    public float range;
    public float damage;
    public float speed;
    public DamageType damageType;
    public bool canJumpSameTarget;

    private List<Enemy> hitTargets = new List<Enemy>();
    public override AttackModifier Clone() {
        return Instantiate(this);
    }
    public override void OnSendModifier(AttackData attackData) {
        //do smth when sent
    }
    public override void OnDeliverModifier(AttackData attackData) {
        if (!canJumpSameTarget)
        {
            hitTargets.Add(attackData.target);
            jumps--;
            if (jumps <= 0) return;
            attackData.target.DealDamage(damage, damageType);
            List<Enemy> targets = EnemyManager.Instance.GetClosestEnemy(attackData.target.transform.position, range);

            foreach(Enemy enemy in hitTargets) {
                targets.Remove(enemy);
            }
            if (targets.Count == 0) return;

            Enemy nextTarget = targets[0];
            AttackData _attackData = new AttackData(
                0, 
                speed, 
                nextTarget, 
                damageType, 
                attackData.towerLogic, 
                this
            );
            ProjectileManager.instance.SpawnProjectile(_attackData, attackData.target.transform.position);
        }
        else 
        {
            hitTargets.Clear();
            hitTargets.Add(attackData.target);
            jumps--;
            if (jumps <= 0) return;
            attackData.target.DealDamage(damage, damageType);
            List<Enemy> targets = EnemyManager.Instance.GetClosestEnemy(attackData.target.transform.position, range);

            foreach(Enemy enemy in hitTargets) {
                targets.Remove(enemy);
            }
            if (targets.Count == 0) return;

            Enemy nextTarget = targets[0];
            AttackData _attackData = new AttackData(
                0, 
                speed, 
                nextTarget, 
                damageType, 
                attackData.towerLogic, 
                this
            );
            ProjectileManager.instance.SpawnProjectile(_attackData, attackData.target.transform.position);
        }
    }
}

