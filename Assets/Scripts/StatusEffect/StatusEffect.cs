using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public float duration;
    public virtual StatusEffect Clone() {
        return null;
    }
    public virtual void ApplyForDuration(AttackData attackData) {
        StatusEffect statusEffect = Clone();
        statusEffect.Apply(attackData);
        attackData.mapLogic.StartCoroutine(RemoveOnTime(attackData, statusEffect));
    }
    public virtual void ApplyUntilRemoved(AttackData attackData) {
        StatusEffect statusEffect = Clone();
        statusEffect.Apply(attackData);
    }
    public virtual void Apply(AttackData attackData) {

    }
    public virtual void Remove(AttackData attackData) {

    }
    private IEnumerator RemoveOnTime(AttackData attackData, StatusEffect statusEffect) {
        while (true) 
        {
            statusEffect.duration -= Time.deltaTime;
            if (statusEffect.duration < 0) {
                statusEffect.Remove(attackData);
                break;
            }
            yield return null;
        }
    }
}
