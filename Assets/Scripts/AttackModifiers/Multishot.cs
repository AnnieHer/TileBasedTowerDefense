using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackModifier", menuName = "Attack Modifiers/Multishot")]
public class Multishot : AttackModifier
{
    public bool ProcModifiers;
    public int targetCount;
    public override AttackModifier Clone() {
        return Instantiate(this);
    }
    public override void OnSendModifier(AttackData attackData) {
        List<Enemy> enemies;
        if (ProcModifiers) 
        {
            enemies = new List<Enemy>(
                EnemyManager.Instance.GetClosestEnemy(
                    attackData.towerLogic.transform.position, 
                    attackData.towerLogic.GetRange() + 0.5f
                )
            );

            enemies.Remove(attackData.target);
            int i = 0;
            foreach(Enemy enemy in enemies) {
                if (i > targetCount) {
                    break;
                }
                List<AttackModifier> clonedModifiers = new List<AttackModifier>();
                foreach (AttackModifier modifier in attackData.attackModifierList)
                {
                    if (modifier == this) continue;
                    clonedModifiers.Add(modifier.Clone());
                }
                AttackData _attackData = new AttackData(
                    attackData.damage, 
                    attackData.speed, 
                    enemies[i], 
                    attackData.damageType, 
                    attackData.towerLogic, 
                    clonedModifiers
                );
                i++;
                ProjectileManager.instance.SpawnProjectile(_attackData, attackData.towerLogic.transform.position, attackData.towerLogic.GetProjectile());
            }
        }
        else 
        {
            enemies = new List<Enemy>(
                EnemyManager.Instance.GetClosestEnemy(
                    attackData.towerLogic.transform.position, 
                    attackData.towerLogic.GetRange() + 0.5f
                )
            );
            enemies.Remove(attackData.target);
            int i = 0;
            foreach(Enemy enemy in enemies) {
                if (i > targetCount) {
                    break;
                }
                AttackData _attackData = new AttackData(
                    attackData.damage,
                    attackData.speed, 
                    enemies[i], 
                    attackData.damageType, 
                    attackData.towerLogic,
                    new List<AttackModifier>()
                );
                i++;
                ProjectileManager.instance.SpawnProjectile(_attackData, attackData.towerLogic.transform.position, attackData.towerLogic.GetProjectile());
            }
        }
    }
    public override void OnDeliverModifier(AttackData attackData) {
        //do smth when hit
    }
}
