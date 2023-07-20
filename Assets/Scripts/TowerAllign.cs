using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAllign : MonoBehaviour
{
    [SerializeField] private Transform _rotar;
    [SerializeField] private Transform _target;
    public void SetTarget(Transform target) {
        _target = target;
    }
    private void Update() {
        _rotar.LookAt(_target);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("triggered!", other.transform);
        other.transform.TryGetComponent<Enemy>(out Enemy enemy);
        _target = enemy.transform;
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform == _target) _target = null;
    }
}
