using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapLogic : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private Vector2[] PathArray;
    private Vector2Int mapSize;
    public void SetMapSize(Vector2 size) {
        mapSize = new Vector2Int((int)size.x, (int)size.y);
    }
    public Vector2Int GetMapSize() {
        return mapSize;
    }
    private int mapID;
    private PlayerManager playerManager;
    public PlayerManager PlayerManager() {
        return playerManager;
    }
    private ProjectileManager projectileManager;
    public ProjectileManager ProjectileManager() {
        return projectileManager;
    }
    private TowerManager towerManager;
    public TowerManager TowerManager() {
        return towerManager;
    }
    private EnemyManager enemyManager;
    public EnemyManager EnemyManager() {
        return enemyManager;
    }

    public event Action<MapLogic> OnMapCleanup;
    public void CleanMap() {
        OnMapCleanup?.Invoke(this);
    }

    void Start()
    {
        playerManager = new PlayerManager();
        playerManager.SetReferenceToMap(this);
        projectileManager = new ProjectileManager();
        projectileManager.SetReferenceToMap(this);
        towerManager = new TowerManager();
        towerManager.SetReferenceToMap(this);
        enemyManager = new EnemyManager();
        enemyManager.SetReferenceToMap(this, enemyPrefab);

        PlayerControls.instance.Init(this);
    }
    public void SendPath(Vector2[] path) {
        PathArray = path;
    }
    public Vector2[] GetPath() {
        return PathArray;
    }
    public void TakeDamage(int value) {
        if (playerManager.ChangeValueHealth(value)) {
            GlobalGet.instance.getPlayerControls().GetComponentByID(1).GetComponent<TMP_Text>().text = "Dead";
        }
    }
}
