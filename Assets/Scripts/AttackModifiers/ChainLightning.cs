using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackModifier", menuName = "Attack Modifiers/ChainLightning")]
public class ChainLightning : AttackModifier
{
    public int jumps;
    public float range;
    [Range(0f, 200f)]
    public int damagePct;
    public float speed;
    public DamageType damageType;
    public bool canJumpSameTarget;
    public ProjectileLogic projectile;

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
            attackData.target.DealDamage(attackData.damage * damagePct / 100f, damageType);
            List<Enemy> targets = attackData.mapLogic.EnemyManager().GetClosestEnemy(attackData.target.transform.position, range);

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
                this,
                attackData.mapLogic
            );
            attackData.mapLogic.ProjectileManager().SpawnProjectile(_attackData, attackData.target.transform.position, projectile);
        }
        else 
        {
            hitTargets.Clear();
            hitTargets.Add(attackData.target);
            jumps--;
            if (jumps <= 0) return;
            attackData.target.DealDamage(damagePct, damageType);
            List<Enemy> targets = attackData.mapLogic.EnemyManager().GetClosestEnemy(attackData.target.transform.position, range);

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
                this,
                attackData.mapLogic
            );
            attackData.mapLogic.ProjectileManager().SpawnProjectile(_attackData, attackData.target.transform.position, projectile);
        }
    }
}

