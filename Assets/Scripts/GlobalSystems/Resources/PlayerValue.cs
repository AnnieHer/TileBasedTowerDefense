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
    public PlayerValue( string valueName, 
                        int value, 
                        int valueDefault, 
                        int valueMax, 
                        int valueMin, 
                        bool canGoOverMax, 
                        bool canGoBelowMin) {
            _valueName = valueName;
            _value = value;
            _valueDefault = valueDefault;
            _valueMax = valueMax;
            _valueMin = valueMin;
            _canGoOverMax = canGoOverMax;
            _canGoBelowMin = canGoBelowMin;
    }
    public void ChangeValue(int change) {
        _value += change;
        if (_value < _valueMin && !_canGoBelowMin) {
            _value = _valueMin;
        }
        if (_value > _valueMax && !_canGoOverMax) {
            _value = _valueMax;
        }
    }
}
