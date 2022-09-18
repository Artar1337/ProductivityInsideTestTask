using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "Scriptables/ScriptableWeapon")]
public class ScriptableWeapon : ScriptableObject
{
    [SerializeField]
    private float damage;
    public float Damage { get => damage; set => damage = value; }
    [SerializeField]
    private float range;
    public float Range { get => range; set => range = value; }
    [SerializeField]
    private new string name;
    public string Name { get => name; set => name = value; }

    public GameObject hitParticles;
}
