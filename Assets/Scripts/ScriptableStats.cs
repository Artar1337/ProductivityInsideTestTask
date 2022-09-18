using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableStats", menuName = "Scriptables/ScriptableStats")]
public class ScriptableStats : ScriptableObject
{
    [SerializeField]
    private float health;
    public float Health { get => health; set => health = value; }
    [SerializeField]
    private float chaseDistance;
    public float ChaseDistance { get => chaseDistance; set => chaseDistance = value; }
    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }
    [SerializeField]
    private Color enemyColor;
    public Color EnemyColor { get => enemyColor; set => enemyColor = value; }
    [SerializeField]
    private new string name;
    public string Name { get => name; set => name = value; }
    private bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }
}
