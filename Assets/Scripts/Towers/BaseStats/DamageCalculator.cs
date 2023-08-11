using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator
{
    private List<DamageModifier> prePercentageModifiers;
    private List<DamageModifier> percentageModifiers;

    public DamageCalculator()
    {
        prePercentageModifiers = new List<DamageModifier>();
        percentageModifiers = new List<DamageModifier>();
    }
    public void AddModifier(DamageModifier modifier)
    {
        if (modifier.Type == ModifierType.PrePercentage)
        {
            prePercentageModifiers.Add(modifier);
        }
        else if (modifier.Type == ModifierType.Percentage)
        {
            percentageModifiers.Add(modifier);
        }
    }
    public float CalculateTotalDamage(float baseDamage)
    {
        float totalDamage = baseDamage;

        foreach (var modifier in prePercentageModifiers)
        {
            totalDamage = modifier.CalculateModifier(totalDamage);
        }

        foreach (var modifier in percentageModifiers)
        {
            totalDamage = modifier.CalculateModifier(totalDamage);
        }

        return totalDamage;
    }
}
