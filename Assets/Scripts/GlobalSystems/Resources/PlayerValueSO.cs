using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Value", menuName = "Player Value/New Player Value")]
public class PlayerValueSO : ScriptableObject
{
    public string valueName;
    public int value;
    public int valueDefault;
    public int valueMax;
    public int valueMin;
    public bool canGoOverMax;
    public bool canGoBelowMin;
    public bool snapToClosest;
}
