using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackModifier", menuName = "Attack Modifiers/Slow Modifier")]
public class SlowModifier : AttackModifier
{
    public StatusEffect slowStatusEffect;
    public override AttackModifier Clone() {
        return Instantiate(this);
    }
    public override void OnDeliverModifier(AttackData attackData) {
        slowStatusEffect.ApplyForDuration(attackData);
    }
    public override void OnEnterModifier(AttackData attackData) {
        slowStatusEffect.ApplyUntilRemoved(attackData);
    }
    public override void OnEndModifier(AttackData attackData) {
        slowStatusEffect.Remove(attackData);
    }
}
