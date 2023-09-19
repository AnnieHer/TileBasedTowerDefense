using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager
{
    private MapLogic _mapLogic;
    private List<TowerLogic> towers = new List<TowerLogic>();
    public void SetReferenceToMap(MapLogic mapLogic) {
        _mapLogic = mapLogic;
        _mapLogic.OnMapCleanup += CleanTowers;
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
        spawnedTower.transform.SetParent(_mapLogic.transform);
        towers.Add(spawnedTower.GetComponent<TowerLogic>());
    }
    private void CleanTowers(MapLogic mapLogic) {
        foreach (TowerLogic towerLogic in towers) {
            GameObject.Destroy(towerLogic.gameObject);
        }
        towers = new List<TowerLogic>();
    }
}
