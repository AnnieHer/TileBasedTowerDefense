using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    [SerializeField] private TowerSO towerSo;
    private float _baseAS, _attackInterval, _baseAttackInterval;
    private float _flatAS, _percentageAS, _totalAS;
    private void Start() {
        _baseAS = towerSo.baseAS;
        _baseAttackInterval = towerSo.baseAttackInterval;
        _totalAS = _baseAS + _flatAS + _baseAS * _percentageAS;
        _attackInterval = _baseAttackInterval / (1 + (_totalAS / 100)); 
    }
    public float GetTotalAS() {
        _totalAS = _baseAS + _flatAS + _baseAS * _percentageAS;
        return _totalAS;
    }
    public void ChangeFlatAS(int change) {
        _flatAS += change;
        GetTotalAS();
    }
    public void ChangePercentageAS(float change) {
        _percentageAS += change;
        GetTotalAS();
    }
}
