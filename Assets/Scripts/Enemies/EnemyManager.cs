using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Enemy enemyPrefab;
    private ObjectPool<Enemy> _pool;
    private List<Vector3> path;
    private List<Enemy> enemyList = new List<Enemy>();

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
            
            Enemy spawnedEnemy = _pool.Get();;
            enemyList.Add(spawnedEnemy);
            spawnedEnemy.SetPath(path);
            spawnedEnemy.OnDeath += HandleEnemyDeath;
        }
    }
    private void HandleEnemyDeath(Enemy enemy) {
        RemoveEnemy(enemy);
        enemy.OnDeath -= HandleEnemyDeath;
    }
    public void AddEnemy(Enemy enemy) {
        enemyList.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy) {
        enemyList.Remove(enemy);
        _pool.Release(enemy);
    }
    public List<Enemy> GetRandomEnemy(int lenght) {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < lenght; i++) {
            enemies.Add(enemyList[Random.Range(0, enemyList.Count)]);
        }
        return enemies;
    }
    public List<Enemy> GetClosestEnemy(Vector3 position, float range) {
        List<Enemy> closestEnemies = new List<Enemy>();
        closestEnemies = enemyList;

        closestEnemies = enemyList
        .Where(x => Vector3.Distance(position, x.transform.position) <= range)
        .OrderBy(x => Vector3.Distance(position, x.transform.position))
        .ToList();

        return closestEnemies;
    }
    
}
