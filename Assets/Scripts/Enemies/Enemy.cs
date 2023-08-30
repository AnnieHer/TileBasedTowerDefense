using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("CombatParams")]
    public float maxHP, currentHP, barrierHP;
    public DamageType barrierType;
    [Header("MoveParams")]
    public float moveSpeed = 1f;
    private float speedModifier = 1f;
    public float driftDuration = 2f;
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

    public event Action<Enemy> OnDeath;
    public void SetPath(List<Vector3> newPath)
    {
        positionOffset = new Vector3(UnityEngine.Random.Range(-0.4f, 0.4f), 0, UnityEngine.Random.Range(-0.4f, 0.4f));
        path = newPath;
        currentPathIndex = 0;
        initialPosition = path[currentPathIndex] + positionOffset;
        targetPosition = path[currentPathIndex + 1] + positionOffset;
        driftTimer = 0f;
        transform.position = path[0];
        currentHP = maxHP;
    }

    private void Update()
    {
        if (currentPathIndex >= path.Count - 1)
        {
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
        if (barrierType == damageType && barrierHP > 0) {
            barrierHP -= damage;
            if (barrierHP < 0) {
                currentHP += barrierHP;
                barrierHP = 0;
            }
        }
        else {
            currentHP -= damage;
        }
        if(currentHP <= 0) {
            currentHP = 0;
            Die();
        }
    }
    private void Die()
    {
        // Вызываем событие с передачей себя как параметра

        OnDeath?.Invoke(this);
        
    }
}
