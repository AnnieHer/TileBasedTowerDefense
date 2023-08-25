using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackModifier", menuName = "Attack Modifiers/Empty Modifier")]
public abstract class AttackModifier : ScriptableObject
{
    public virtual AttackModifier Clone() {
        return null;
    }
    public virtual void OnSendModifier(AttackData attackData) {
        //do smth when sent
    }
    public virtual void OnDeliverModifier(AttackData attackData) {
        //do smth when hit
    }
}

