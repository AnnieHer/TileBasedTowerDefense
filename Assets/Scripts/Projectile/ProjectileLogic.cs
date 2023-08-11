using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    protected AttackData _attackData;
    
    public void Init(AttackData attackData) {
        _attackData = attackData;
        _attackData.target.OnDeath += HandleEnemyDeath;
        foreach (AttackModifier attackModifier in _attackData.attackModifierList) {
            attackModifier.OnSendModifier(_attackData);
        }
    }
    private void HandleEnemyDeath(Enemy enemy) {
        Destroy(gameObject);
        enemy.OnDeath -= HandleEnemyDeath;
    }
    void Update()
    {
        if (_attackData.target == null) return;
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
        foreach (AttackModifier attackModifier in _attackData.attackModifierList) {
            attackModifier.OnDeliverModifier(_attackData);
        }
        _attackData.target.OnDeath -= HandleEnemyDeath;
        _attackData.target.DealDamage(_attackData.damage);
        
        Destroy(gameObject);
    }
}
