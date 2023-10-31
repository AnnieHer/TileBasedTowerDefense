public enum DamageType {
    physical = 0,
    magical = 1,
    pure = 2,
}
public enum ModificationType {
    Base = 0,
    Flat = 1,
    Percentage = 2
}
public enum TargetType {
    nonTarget = 0,
    target = 1,
    point = 2,
    direction = 3,
}
public enum TargetSelection {
    first = 0,
    last = 1,
    closest = 2,
    strongest = 3,
    weakest = 4
}
[System.Serializable]
public enum ModifierQueueType
{
    PrePercentage,
    Percentage
}
[System.Serializable]
public enum ModifierStatsType
{
    Damage,
    AttackSpeed,
    AttackRange
}
[System.Serializable]
public class Stats {
    public float damage;
    public float attackSpeed;
    public float attackRange;
}
public enum Rarity 
{
    common,
    rare,
    special,
    epic,
    legendary
}
public enum GameState
{
    pause,
    gameOn,
    gameOver,
}