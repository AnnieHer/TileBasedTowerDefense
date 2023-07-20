using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Enemy enemyPrefab;
    private List<Vector3> path;

    private void Awake() {
        Instance = this;
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
        Vector2[] pathArray = GridManager.Instance.GetPath();
        path = new List<Vector3>();
        foreach (Vector2 point in pathArray)
        {
            Vector3 position = new Vector3(point.x, 0.5f, point.y);
            path.Add(position);
        }
        Enemy spawnedEnemy = Instantiate(enemyPrefab);
        spawnedEnemy.SetPath(path);
        }
    }
}
