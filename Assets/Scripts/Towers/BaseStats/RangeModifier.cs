using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeModifier
{
    public abstract ModifierType Type {get; }
    public abstract float CalculateModifier(float baseValue);
}
public class PercentageRangeModifier : ASModifier
{
    private float percentage;
    public override ModifierType Type => ModifierType.Percentage;

    public PercentageRangeModifier(float percentage)
    {
        this.percentage = percentage;
    }

    public override float CalculateModifier(float baseValue)
    {
        return baseValue * (1 + percentage);
    }

}
public class FlatRangeModifier : ASModifier
{
    private float flat;
    public override ModifierType Type => ModifierType.PrePercentage;

    public FlatRangeModifier(float flat)
    {
        this.flat = flat;
    }

    public override float CalculateModifier(float baseValue)
    {
        return flat;
    }
}
