using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] private TowerSO towerSo;
    private float _baseRange;
    private float _flatRange, _percentageRange, _totalRange;
    private void Start() {
        _baseRange = towerSo.baseRange;
        _totalRange = _baseRange + _flatRange + _baseRange * _percentageRange;
    }
    public float GetTotalRange() {
        _totalRange = _baseRange + _flatRange + _baseRange * _percentageRange;
        return _totalRange;
    }
    public void ChangeFlatRange(int change) {
        _flatRange += change;
        GetTotalRange();
    }
    public void ChangePercentageRange(float change) {
        _percentageRange += change;
        GetTotalRange();
    }
}
