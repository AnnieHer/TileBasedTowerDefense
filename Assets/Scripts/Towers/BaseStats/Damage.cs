using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private TowerSO towerSo;
    private int _baseDamage;
    private int _flatDamage;
    private float _percentageBaseDamage, _percentageAllDamage, _totalDamage, _maxPercentageHP, _currentPercentageHP;
    private DamageType _damageType;
    private Enemy currentTarget;
    private enum DamageType {
        physical,
        magical,
        pure,
        none
    }
    private void Start() {
        _baseDamage = towerSo.baseDamage;
        _damageType = (DamageType)towerSo.damageType;
        _totalDamage = (_baseDamage + _flatDamage + _baseDamage * _percentageBaseDamage) * _percentageAllDamage;
    }
    public float GetTotalDamage() {
        _totalDamage = (_baseDamage + _flatDamage + _baseDamage * _percentageBaseDamage + currentTarget.maxHP * _maxPercentageHP + currentTarget.currentHP * _currentPercentageHP) * _percentageAllDamage;
        return _totalDamage;
    }
    public void ChangeFlatDamage(int change) {
        _flatDamage += change;
        GetTotalDamage();
    }
    public void ChangePercentageBaseDamage(float change) {
        _percentageBaseDamage += change;
        GetTotalDamage();
    }
    public void ChangePercentageAllDamage(float change) {
        _percentageAllDamage += change;
        GetTotalDamage();
    }
}
