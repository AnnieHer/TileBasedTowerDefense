using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("CombatParams")]
    public float maxHP, currentHP;
    [Header("MoveParams")]
    public float moveSpeed = 1f;
    public float driftDuration = 2f;
    private float summarySpeed;
    private List<Vector3> path;
    private int currentPathIndex = 0;
    private float driftTimer = 0f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public void SetPath(List<Vector3> newPath)
    {
        path = newPath;
        currentPathIndex = 0;
        initialPosition = path[currentPathIndex];
        targetPosition = path[currentPathIndex + 1];
        driftTimer = 0f;
        transform.position = path[0];
    }

    private void Update()
    {
        if (currentPathIndex >= path.Count - 1)
        {
            Destroy(this.gameObject);
            // Unit has reached the end of the path or desired condition met
            // Perform any required actions or destroy the unit
            return;
        }

        driftTimer += Time.deltaTime;
        summarySpeed = driftDuration + driftDuration * (1 - moveSpeed);
        float t = driftTimer / summarySpeed;
        transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

        if (driftTimer >= summarySpeed)
        {
            currentPathIndex++;
            if (currentPathIndex < path.Count - 1)
            {
                initialPosition = path[currentPathIndex];
                targetPosition = path[currentPathIndex + 1];
                driftTimer = 0f;
            }
        }
    }
    public void ChangeMoveSpeed(float speed) {
        moveSpeed += speed;
        driftTimer = driftTimer * (1 - moveSpeed);
    }
}
