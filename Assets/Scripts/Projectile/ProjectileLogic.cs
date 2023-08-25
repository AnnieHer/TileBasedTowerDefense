using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    protected AttackData _attackData;
    public void Init(AttackData attackData) {
        _attackData = attackData;
        foreach(Enemy enemy in _attackData.targets) {
            enemy.OnDeath += HandleEnemyDeath;
        }
        if (_attackData.attackModifierList.Count != 0) {
            foreach (AttackModifier attackModifier in _attackData.attackModifierList) {
                attackModifier.OnSendModifier(_attackData);
            }
        }
        _attackData.target = _attackData.targets[0];
    }
    private void HandleEnemyDeath(Enemy enemy) {
        _attackData.targets.Remove(enemy);
        enemy.OnDeath -= HandleEnemyDeath;
    }
    void Update()
    {
        if (_attackData.targets == null) return;
        float step = _attackData.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _attackData.target.transform.position, step);

        float distanceToTarget = Vector3.Distance(transform.position, _attackData.target.transform.position);

        if (distanceToTarget < 0.3f) // Подберите подходящее значение для вашей игры
        {
            // Снаряд достиг цели
            TargetHit();
            
        }
    }
    private void TargetHit() {
        if (_attackData.attackModifierList.Count != 0) {
            foreach (AttackModifier attackModifier in _attackData.attackModifierList) {
                attackModifier.OnDeliverModifier(_attackData);
            }
        }
        _attackData.target.OnDeath -= HandleEnemyDeath;
        _attackData.target.DealDamage(_attackData.damage, _attackData.damageType);

        _attackData.targets.Remove(_attackData.target);
        if (_attackData.targets.Count != 0) {
        _attackData.target = _attackData.targets[0];
        }
        else Destroy(this.gameObject);
    }
}
