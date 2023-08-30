using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Upgrade Unit", menuName = "Upgrade Unit/New upgrade")]
public class UpgradeUnit : ScriptableObject
{
    public int cost;
    public Rarity rarity;
    public List<StatsModifier> statsModifiers;
    public List<StatsModifier> GetModifiers() {
        return statsModifiers;
    }
}
