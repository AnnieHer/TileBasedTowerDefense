using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private TowerSO towerSo;
    public Damage damage;
    private int _baseCost;
    private string _name;
    private int _totalCost, _sellCost;

    private void Start() {
        _baseCost = towerSo.baseCost;
        _name = towerSo.towerName;
        _totalCost = _baseCost;
        _sellCost = _totalCost / 2;
    }
    private int getSellCost() {
        _sellCost = _totalCost / 2;
        return _sellCost;
    }
    private void calculateTotalCost(int upgradeCost) {
        _totalCost += upgradeCost;
    }
}
