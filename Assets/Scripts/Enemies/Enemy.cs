using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("CombatParams")]
    public float _maxHP, _currentHP, _barrierHP;
    public DamageType _barrierType;
    [Header("MoveParams")]
    public float _moveSpeed = 1f;
    private float speedModifier = 1f;
    public float defaultSpeed = 2f;
    public float driftDuration;
    private List<Vector3> path;
    private int currentPathIndex = 0;
    private float driftTimer = 0f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float distanceTravelled{
        get;
        private set;
    } 
    private Vector3 positionOffset;
    private MapLogic _mapLogic;

    public event Action<Enemy> OnDeath;
    public void Init(EnemySO enemySO, MapLogic mapLogic) {
        _maxHP = enemySO.maxHealth;
        _barrierHP = enemySO.barrierHealth;
        _moveSpeed = enemySO.speed;
        _barrierType = enemySO.barrierType;
        _currentHP = _maxHP;
        driftDuration = defaultSpeed / _moveSpeed;
        _mapLogic = mapLogic;
    }
    public void SetPath(List<Vector3> newPath)
    {
        positionOffset = new Vector3(UnityEngine.Random.Range(-0.4f, 0.4f), 0, UnityEngine.Random.Range(-0.4f, 0.4f));
        path = newPath;
        currentPathIndex = 0;
        initialPosition = path[currentPathIndex] + positionOffset;
        targetPosition = path[currentPathIndex + 1] + positionOffset;
        driftTimer = 0f;
        transform.position = path[0];
    }

    private void Update()
    {
        if (currentPathIndex >= path.Count - 1)
        {
            _mapLogic.TakeDamage(-(int)_currentHP);
            Die();
            //Destroy(gameObject);
            // Unit has reached the end of the path or desired condition met
            // Perform any required actions or destroy the unit
            return;
        }

        driftTimer += Time.deltaTime * speedModifier;
        float t = driftTimer / driftDuration;
        distanceTravelled += t;
        transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

        if (driftTimer >= driftDuration)
        {
            currentPathIndex++;
            if (currentPathIndex < path.Count - 1)
            {
                initialPosition = path[currentPathIndex] + positionOffset;
                targetPosition = path[currentPathIndex + 1] + positionOffset;
                driftTimer = 0f;
            }
        }
    }
    public void ApplySlow(float speed) {
        speedModifier *= speed;
    }
    public void DealDamage(float damage, DamageType damageType) {
        if (_barrierType == damageType && _barrierHP > 0) {
            _barrierHP -= damage;
            if (_barrierHP < 0) {
                _currentHP += _barrierHP;
                _barrierHP = 0;
            }
        }
        else {
            _currentHP -= damage;
        }
        if(_currentHP <= 0) {
            _currentHP = 0;
            Die();
        }
    }
    private void Die()
    {
        // Вызываем событие с передачей себя как параметра

        OnDeath?.Invoke(this);
        
    }
}
