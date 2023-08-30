using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    void Awake()
    {
        instance = this;
    }
    public ProjectileLogic SpawnProjectile(AttackData attackData, Vector3 position, ProjectileLogic projectileLogic) {
        ProjectileLogic projectile = Instantiate(projectileLogic, position, Quaternion.identity);
        projectile.Init(attackData);
        return projectile;
    }
}
