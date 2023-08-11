using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModifier
{
    public virtual void OnDeliverModifier(AttackData attackData) {
        //empty
    }
    public virtual void OnSendModifier(AttackData attackData) {
        //empty
    }
    
}
