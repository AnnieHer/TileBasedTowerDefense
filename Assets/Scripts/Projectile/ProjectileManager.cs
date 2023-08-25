using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    [SerializeField] private ProjectileLogic projectileLogic;
    void Awake()
    {
        instance = this;
    }
    public ProjectileLogic SpawnProjectile(AttackData attackData, Vector3 position) {
        ProjectileLogic projectile = Instantiate(projectileLogic, position, Quaternion.identity);
        projectile.Init(attackData);
        return projectile;
    }
}
