using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Enemy enemyPrefab;
    private ObjectPool<Enemy> _pool;
    private List<Vector3> path;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        _pool = new ObjectPool<Enemy>(() => {
            return Instantiate(enemyPrefab);
        }, Enemy => {
            Enemy.gameObject.SetActive(true);
        }, Enemy => {
            Enemy.gameObject.SetActive(false);
        }, Enemy => {
            Destroy(Enemy.gameObject);
        }, false, 100, 100);
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
