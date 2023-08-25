using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    [Header("Serializable Object stats")]
    [SerializeField] protected TowerSO towerSo;

    protected DamageType _damageType;
    protected float _baseAS, _baseDamage, _baseRange, _projectileSpeed, _baseCost, _baseAttackInterval;

    protected float _totalAS, _totalDamage, _totalRange;
    public float GetRange() {
        return _totalRange;
    }
    
    protected DamageCalculator _DamageCalculator;
    protected ASCalculator _ASCalculator;
    protected RangeCalculator _RangeCalculator;

    [SerializeField] protected List<AttackModifier> attackModifiers = new List<AttackModifier>(); 
    
    public float attackInterval {
        get; 
        private set;
    }

    protected List<Enemy> _inRangeEnemies = new List<Enemy>();
    protected Enemy _currentTarget;
    protected TargetSelection _targetSelection;
    protected TargetType _targetType;

    protected SphereCollider _sphereCollider;
    
    
    private void Start() {
        _sphereCollider = GetComponent<SphereCollider>();

        _baseDamage = towerSo.baseDamage;
        _baseAS = towerSo.baseAS;
        _baseRange = towerSo.baseRange;
        _baseCost = towerSo.baseCost;
        _baseAttackInterval = towerSo.baseAttackInterval;   
        _projectileSpeed = towerSo.projectileSpeed;

        _damageType = towerSo.damageType;
        _targetType = towerSo.targetType;

        attackInterval = _baseAttackInterval / (1 + (_totalAS / 100));

        _totalDamage = _baseDamage;
        _totalAS = _baseAS;
        _totalRange = _baseRange;

        _sphereCollider.radius = _totalRange;
        StartCoroutine(Tick());
        StartCoroutine(ShootProjectiles());
    }
    private IEnumerator Tick() {
        while (true) {
            if (_currentTarget != null)
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) > _totalRange) _currentTarget = null;
            UpdateTarget();
            yield return new WaitForSeconds(0.2f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Add(enemy);
            enemy.OnDeath += HandleEnemyDeath;
            UpdateTarget();
        }
    }
    private void HandleEnemyDeath(Enemy enemy) {
        _inRangeEnemies.Remove(enemy);
        UpdateTarget();
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
            _inRangeEnemies.Remove(enemy);
            UpdateTarget();
        }
    }
    void Update()
    {
        
    }
    
    private IEnumerator ShootProjectiles()
    {
        while (true)
        {
            if (_currentTarget != null)
            Shoot();
            yield return new WaitForSeconds(attackInterval);
        }
    }
    public void ChangeTargettingSelection(TargetSelection targetSelection) {
        _targetSelection = targetSelection;
        UpdateTarget();
    }

    private void Shoot()
    {
        List<AttackModifier> clonedModifiers = new List<AttackModifier>();

        foreach (AttackModifier modifier in attackModifiers)
        {
            clonedModifiers.Add(modifier.Clone());
        }
        AttackData attackData = new AttackData(_totalDamage, _projectileSpeed, _currentTarget, _damageType ,this, clonedModifiers);
        ProjectileManager.instance.SpawnProjectile(attackData, transform.position);
    }
    private void UpdateTarget() {

        if (_inRangeEnemies.Count == 0) {
            _currentTarget = null;
            return;
        }
        switch (_targetSelection) {
            case TargetSelection.first: 
                _currentTarget = _inRangeEnemies[0];
            break;
                
            case TargetSelection.last: 
                _currentTarget = _inRangeEnemies[^1];
            break;
            
            case TargetSelection.closest: 
                Enemy closestEnemy;
                float distance;
                float closestDistance = _totalRange;
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
