using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAura : TowerLogic
{
    protected override void OnTriggerEnter(Collider other) {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Add(enemy);
            enemy.OnDeath += HandleEnemyDeath;

            List<AttackModifier> clonedModifiers = new List<AttackModifier>();

            foreach (AttackModifier modifier in _attackModifiers)
            {
                clonedModifiers.Add(modifier.Clone());
            }
            AttackData attackData = new AttackData(_totalStats.damage, _projectileSpeed, enemy, _damageType ,this, clonedModifiers);
            ProjectileManager.instance.SpawnProjectile(attackData, enemy.transform.position, _projectile);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Remove(enemy);

            List<AttackModifier> clonedModifiers = new List<AttackModifier>();

            foreach (AttackModifier modifier in _attackModifiers)
            {
                clonedModifiers.Add(modifier.Clone());
            }
            AttackData attackData = new AttackData(_totalStats.damage, _projectileSpeed, enemy, _damageType ,this, clonedModifiers);
            ProjectileManager.instance.SpawnProjectile(attackData, enemy.transform.position, _projectile);
        }
    }
    protected override IEnumerator ShootProjectiles()
    {
        yield return null;
    }
    protected override void UpdateTarget() {

    }
    protected override void Shoot() {
        
    }
}
