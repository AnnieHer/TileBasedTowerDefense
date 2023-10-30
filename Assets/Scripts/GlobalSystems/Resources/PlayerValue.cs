using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerValue
{
    [SerializeField] protected string _valueName;
    [SerializeField] protected int _value;
    [SerializeField] protected int _valueDefault;
    [SerializeField] protected int _valueMax;
    [SerializeField] protected int _valueMin;
    [SerializeField] protected bool _canGoOverMax;
    [SerializeField] protected bool _canGoBelowMin;
    [SerializeField] protected bool _snapToClosest;
    public PlayerValue( string valueName, 
                        int value, 
                        int valueDefault, 
                        int valueMax, 
                        int valueMin, 
                        bool canGoOverMax, 
                        bool canGoBelowMin,
                        bool snapToClosest) {
            _valueName = valueName;
            _value = value;
            _valueDefault = valueDefault;
            _valueMax = valueMax;
            _valueMin = valueMin;
            _canGoOverMax = canGoOverMax;
            _canGoBelowMin = canGoBelowMin;
            _snapToClosest = snapToClosest;
    }
    public PlayerValue(PlayerValueSO playerValueSO) {
            _valueName = playerValueSO.valueName;
            _value = playerValueSO.value;
            _valueDefault = playerValueSO.valueDefault;
            _valueMax = playerValueSO.valueMax;
            _valueMin = playerValueSO.valueMin;
            _canGoOverMax = playerValueSO.canGoOverMax;
            _canGoBelowMin = playerValueSO.canGoBelowMin;
            _snapToClosest = playerValueSO.snapToClosest;
    }
    public bool ChangeValue(int change) {
        if (_value + change < _valueMin && !_canGoBelowMin) {
            return false;
        }
        if (_value + change > _valueMax && !_canGoOverMax) {
            return false;
        }
        if (_value + change < _valueMin && _snapToClosest) {
            _value = _valueMin;
        }
        if (_value + change > _valueMax && _snapToClosest) {
            _value = _valueMax;
        }
        _value += change;
        return true;
    }
}
