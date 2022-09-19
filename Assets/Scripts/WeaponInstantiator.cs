using UnityEngine;

// handles weapon visuals

public class WeaponInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    private Animator weaponAnimator;

    public void InstantiateWeapon()
    {
        if (weapon != null)
            weaponAnimator = Instantiate(weapon, transform).GetComponent<Animator>();
    }

    public void WeaponSwoosh()
    {
        weaponAnimator.SetTrigger("Hit");
    }
}
