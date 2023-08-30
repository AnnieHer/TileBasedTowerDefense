using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerLogic : MonoBehaviour
{
    [Header("Serializable Object stats")]
    [SerializeField] protected TowerSO towerSo;

    protected DamageType _damageType;
    protected float _projectileSpeed, _baseCost, _baseAttackTime;
    protected ProjectileLogic _projectile;
    public ProjectileLogic GetProjectile() {
        return _projectile;
    }

    protected Stats _baseStats;
    protected Stats _totalStats;
    public float GetRange() {
        return _totalStats.attackRange;
    }
    
    

    protected int _maxModifiers;
    protected List<AttackModifier> attackModifiers = new List<AttackModifier>(); 
    protected int _maxUpgrades;
    protected StatsCalculator _StatsCalculator;
    protected List<UpgradeUnit> _upgrades;
    
    public float attackInterval {
        get; 
        private set;
    }

    protected List<Enemy> _inRangeEnemies = new List<Enemy>();
    protected Enemy _currentTarget;
    protected TargetSelection _targetSelection;
    protected TargetType _targetType;

    protected SphereCollider _sphereCollider;
    
    
    protected virtual void Start() {
        _sphereCollider = GetComponent<SphereCollider>();

        _baseStats.damage = towerSo.baseDamage;
        _baseStats.attackSpeed = towerSo.baseAS;
        _baseStats.attackRange = towerSo.baseRange;
        _baseCost = towerSo.baseCost;
        _baseAttackTime = towerSo.baseAttackTime;   
        _projectileSpeed = towerSo.projectileSpeed;
        _projectile = towerSo.projectile;

        _damageType = towerSo.damageType;
        _targetType = towerSo.targetType;

        _totalStats.damage = _baseStats.damage;
        _totalStats.attackSpeed = _baseStats.attackSpeed;
        _totalStats.attackRange = _baseStats.attackRange;

        attackModifiers = new List<AttackModifier>();
        foreach(AttackModifier attackModifier in towerSo.attackModifiers) {
            attackModifiers.Add(attackModifier.Clone());
        }

        attackInterval = _baseAttackTime / (1 + (_totalStats.attackSpeed / 100));

        _sphereCollider.radius = _totalStats.attackRange;
        StartCoroutine(Tick());
        StartCoroutine(ShootProjectiles());
    }
    public virtual void AddModifier(AttackModifier attackModifier) {
        if (attackModifiers.Count < _maxModifiers) {
            attackModifiers.Add(attackModifier);
        }
    }
    public virtual void RemoveModifier(AttackModifier attackModifier) {
        attackModifiers.Remove(attackModifier);
    }
    public virtual void AddUpgrade(UpgradeUnit upgradeUnit) {
        _upgrades.Add(upgradeUnit);
        foreach(StatsModifier statsModifier in upgradeUnit.GetModifiers())
        _StatsCalculator.AddModifier(statsModifier);
    }
    public virtual void RemoveUpgrade(UpgradeUnit upgradeUnit) {
        _upgrades.Remove(upgradeUnit);
        foreach(StatsModifier statsModifier in upgradeUnit.GetModifiers())
        _StatsCalculator.RemoveModofier(statsModifier);
    }
    protected virtual IEnumerator Tick() {
        while (true) {
            if (_currentTarget != null)
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) > _totalStats.attackRange) _currentTarget = null;
            UpdateTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }
    protected virtual void HandleEnemyDeath(Enemy enemy) {
        _inRangeEnemies.Remove(enemy);
        UpdateTarget();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Add(enemy);
            enemy.OnDeath += HandleEnemyDeath;
            UpdateTarget();
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Remove(enemy);
            UpdateTarget();
        }
    }
    
    protected virtual IEnumerator ShootProjectiles()
    {
        while (true)
        {
            if (_currentTarget != null)
            Shoot();
            yield return new WaitForSeconds(attackInterval);
        }
    }
    public virtual void ChangeTargettingSelection(TargetSelection targetSelection) {
        _targetSelection = targetSelection;
        UpdateTarget();
    }

    protected virtual void Shoot()
    {
        List<AttackModifier> clonedModifiers = new List<AttackModifier>();

        foreach (AttackModifier modifier in attackModifiers)
        {
            clonedModifiers.Add(modifier.Clone());
        }
        AttackData attackData = new AttackData(_totalStats.damage, _projectileSpeed, _currentTarget, _damageType ,this, clonedModifiers);
        ProjectileManager.instance.SpawnProjectile(attackData, transform.position, _projectile);
    }
    protected virtual void UpdateTarget() {

        if (_inRangeEnemies.Count == 0) {
            _currentTarget = null;
            return;
        }
        switch (_targetSelection) {
            case TargetSelection.first: 
                _currentTarget = _inRangeEnemies.OrderBy(
                    x => x.distanceTravelled
                ).ToList()[^1];
            break;
                
            case TargetSelection.last: 
                _currentTarget = _inRangeEnemies.OrderBy(
                    x => x.distanceTravelled
                ).ToList()[0];;
            break;
            
            case TargetSelection.closest: 
                Enemy closestEnemy;
                float distance;
                float closestDistance = _totalStats.attackRange;
                foreach (Enemy enemy in _inRangeEnemies) {
                    distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance) {
                        closestEnemy = enemy;
                    }
                }
            break;

            case TargetSelection.strongest: 
                float maxHP = 0;
                foreach (Enemy enemy in _inRangeEnemies) {
                    if (enemy.currentHP > maxHP) {
                        maxHP = enemy.currentHP;
                        _currentTarget = enemy;
                    }
                }
            break;

            case TargetSelection.weakest: 
                float minHP = 0;
                foreach (Enemy enemy in _inRangeEnemies) {
                    if (enemy.currentHP < minHP) {
                        maxHP = enemy.currentHP;
                        _currentTarget = enemy;
                    }
                }
            break;
        }
    }
}
