using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectInstance : MonoBehaviour
{
    public static StatusEffectInstance instance;
    void Awake()
    {
        instance = this;
    }
}
