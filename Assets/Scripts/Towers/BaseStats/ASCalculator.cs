using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASCalculator
{
    private List<ASModifier> prePercentageModifiers;
    private List<ASModifier> percentageModifiers;

    public ASCalculator()
    {
        prePercentageModifiers = new List<ASModifier>();
        percentageModifiers = new List<ASModifier>();
    }
    public void AddModifier(ASModifier modifier)
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
    public float CalculateTotalAS(float baseAS)
    {
        float totalAS = baseAS;

        foreach (var modifier in prePercentageModifiers)
        {
            totalAS = modifier.CalculateModifier(totalAS);
        }

        foreach (var modifier in percentageModifiers)
        {
            totalAS = modifier.CalculateModifier(totalAS);
        }

        return totalAS;
    }
}
