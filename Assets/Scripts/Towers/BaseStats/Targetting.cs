using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    [SerializeField] private TowerSO towerSo;
    private TargetType _targetType;
    private int _targetCount;
    private enum TargetType {
        nonTarget,
        target,
        point,
        direction,
    }
    private void Start() {
        _targetType = (TargetType)towerSo.targetType;
        _targetCount = towerSo.targetCount;
    }
}
