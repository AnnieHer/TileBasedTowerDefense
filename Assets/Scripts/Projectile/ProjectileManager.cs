using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    private MapLogic _mapLogic;
    public void SetReferenceToMap(MapLogic mapLogic) {
        _mapLogic = mapLogic;
    }
    public ProjectileLogic SpawnProjectile(AttackData attackData, Vector3 position, ProjectileLogic projectileLogic) {
        ProjectileLogic projectile = GameObject.Instantiate(projectileLogic, position, Quaternion.identity);
        projectile.Init(attackData);
        return projectile;
    }
}
