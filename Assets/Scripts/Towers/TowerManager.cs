using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager
{
    private MapLogic _mapLogic;
    public void SetReferenceToMap(MapLogic mapLogic) {
        _mapLogic = mapLogic;
    }
    public void SpawnTower(Vector3 position, TowerSO towerSO) {
        GameObject spawnedTower;
        spawnedTower = GameObject.Instantiate(towerSO.prefab, position + Vector3.up, Quaternion.identity);
        if (towerSO.targetType == TargetType.target){
            spawnedTower.AddComponent<TowerSentry>();
        }
        if (towerSO.targetType == TargetType.nonTarget) {
            spawnedTower.AddComponent<TowerAura>();
        }
        spawnedTower.GetComponent<TowerLogic>().Init(towerSO, _mapLogic);
    }
}
