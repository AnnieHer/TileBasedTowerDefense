using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    void Awake()
    {
        instance = this;
    }
    public void SpawnTower(Vector3 position, TowerSO towerSO) {
        GameObject spawnedTower;
        spawnedTower = Instantiate(towerSO.prefab, position + Vector3.up, Quaternion.identity);
        if (towerSO.targetType == TargetType.target){
            spawnedTower.AddComponent<TowerSentry>();
        }
        if (towerSO.targetType == TargetType.nonTarget) {
            spawnedTower.AddComponent<TowerAura>();
        }
        spawnedTower.GetComponent<TowerLogic>().Init(towerSO);
    }
}
