using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
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
    void Start()
    {
        playerManager = new PlayerManager();
        projectileManager = new ProjectileManager();
        towerManager = new TowerManager();
        enemyManager = new EnemyManager();

        PlayerControls.instance.Init(this);
    }
}
