using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsModifier
{
    public float value;
    public ModifierQueueType queueType;
    public ModifierStatsType statsType;
    public StatsModifier(float val, ModifierQueueType modifierQueueType, ModifierStatsType modifierStatsType) {
        value = val;
        queueType = modifierQueueType;
        statsType = modifierStatsType;
    }
    public float CalculateModifier(float baseValue) {
        if (queueType == ModifierQueueType.PrePercentage) {
            return value;
        }
        else {
            return baseValue * (1 + value);
        }
    }
}