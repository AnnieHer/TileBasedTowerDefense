using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCalculator
{
    private List<StatsModifier> prePercentageModifiers;
    private List<StatsModifier> percentageModifiers;

    public StatsCalculator()
    {
        prePercentageModifiers = new List<StatsModifier>();
        percentageModifiers = new List<StatsModifier>();
    }
    public void AddModifier(StatsModifier modifier)
    {
        if (modifier.queueType == ModifierQueueType.PrePercentage)
        {
            prePercentageModifiers.Add(modifier);
        }
        else if (modifier.queueType == ModifierQueueType.Percentage)
        {
            percentageModifiers.Add(modifier);
        }
    }
    public void RemoveModofier(StatsModifier modifier) {
        if (modifier.queueType == ModifierQueueType.PrePercentage)
        {
            prePercentageModifiers.Remove(modifier);
        }
        else if (modifier.queueType == ModifierQueueType.Percentage)
        {
            percentageModifiers.Remove(modifier);
        }
    }
    public Stats CalculateTotalStats(Stats baseStats)
    {
        Stats totalStats = new Stats
        {
            damage = baseStats.damage,
            attackSpeed = baseStats.attackSpeed,
            attackRange = baseStats.attackRange
        };

        foreach (ModifierStatsType statType in Enum.GetValues(typeof(ModifierStatsType)))
        {
            float baseValue = GetStatValue(statType, baseStats);
            float totalStatValue = baseValue;

            foreach (var modifier in prePercentageModifiers)
            {
                if (modifier.statsType == statType)
                {
                    totalStatValue = modifier.CalculateModifier(totalStatValue);
                }
            }

            foreach (var modifier in percentageModifiers)
            {
                if (modifier.statsType == statType)
                {
                    totalStatValue = modifier.CalculateModifier(totalStatValue);
                }
            }
             
            switch (statType)
            {
                case ModifierStatsType.Damage:
                    totalStats.damage = totalStatValue;
                    break;
                case ModifierStatsType.AttackSpeed:
                    totalStats.attackSpeed = totalStatValue;
                    break;
                case ModifierStatsType.AttackRange:
                    totalStats.attackRange = totalStatValue;
                    break;
            }
        }
        return totalStats;
    }
        
    private float GetStatValue(ModifierStatsType statType, Stats stats)
    {
        switch (statType)
        {
            case ModifierStatsType.Damage:
                return stats.damage;
            case ModifierStatsType.AttackSpeed:
                return stats.attackSpeed;
            case ModifierStatsType.AttackRange:
                return stats.attackRange;
            default:
                return 0f; // Возможно, нужно вернуть какое-то дефолтное значение
        }
    }

}
