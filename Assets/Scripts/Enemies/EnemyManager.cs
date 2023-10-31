using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Pool;

public class EnemyManager
{
    private Enemy enemyPrefab;
    private ObjectPool<Enemy> _pool;
    private List<Vector3> path;
    private List<Enemy> enemyList = new List<Enemy>();
    private MapLogic _mapLogic;
    
    public void SetReferenceToMap(MapLogic mapLogic, Enemy enemy) {
        enemyPrefab = enemy;
        _mapLogic = mapLogic;
        _mapLogic.OnMapCleanup += ClearEnemies;
        _pool = new ObjectPool<Enemy>(() => {
            return GameObject.Instantiate(enemyPrefab);
        }, Enemy => {
            Enemy.gameObject.SetActive(true);
        }, Enemy => {
            Enemy.gameObject.SetActive(false);
        }, Enemy => {
            GameObject.Destroy(Enemy.gameObject);
        }, false, 100, 100);
    }
    private void ClearEnemies(MapLogic mapLogic) {
        foreach (Enemy enemy in enemyList) {
            _pool.Release(enemy);
        }
        enemyList = new List<Enemy>();
    }
    
    public void SpawnEnemy(EnemySO enemySO) {
        Vector2[] pathArray = _mapLogic.GetPath();
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
        spawnedEnemy.Init(enemySO, _mapLogic);
        spawnedEnemy.transform.SetParent(_mapLogic.transform);
    }
    private void HandleEnemyDeath(Enemy enemy) {
        RemoveEnemy(enemy);
        enemy.OnDeath -= HandleEnemyDeath;
    }
    public void AddEnemy(Enemy enemy) {
        enemyList.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy) {
        _pool.Release(enemy);
        enemyList.Remove(enemy);
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
