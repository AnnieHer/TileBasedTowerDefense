using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerLogic tower;
    public static TowerManager instance;
    void Awake()
    {
        instance = this;
    }
    public void SpawnTower(Vector3 position) {
        Instantiate(tower, position + Vector3.up, Quaternion.identity);
    }
}
