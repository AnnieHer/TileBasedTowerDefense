using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    PrePercentage,
    Percentage
}
public abstract class DamageModifier
{
    public abstract ModifierType Type {get; }
    public abstract float CalculateModifier(float baseValue);

}
public class PercentageDamageModifier : DamageModifier
{
    private float percentage;
    public override ModifierType Type => ModifierType.Percentage;

    public PercentageDamageModifier(float percentage)
    {
        this.percentage = percentage;
    }

    public override float CalculateModifier(float baseValue)
    {
        return baseValue * (1 + percentage);
    }

}
public class FlatDamageModifier : DamageModifier
{
    private float flat;
    public override ModifierType Type => ModifierType.PrePercentage;

    public FlatDamageModifier(float flat)
    {
        this.flat = flat;
    }

    public override float CalculateModifier(float baseValue)
    {
        return flat;
    }
}