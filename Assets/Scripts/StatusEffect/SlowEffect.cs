using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new slow effect", menuName = "Status Effects/Slow Status Effect")]
public class SlowEffect : StatusEffect
{
    [Range(0f, 100f)]
    public float slowPct;
    public override StatusEffect Clone() {
        return Instantiate(this);
    }
    public override void Apply(AttackData attackData) {
        attackData.target.ApplySlow(slowPct/100);
    }
    public override void Remove(AttackData attackData) {
        attackData.target.ApplySlow(1/(slowPct/100));
    }
}
