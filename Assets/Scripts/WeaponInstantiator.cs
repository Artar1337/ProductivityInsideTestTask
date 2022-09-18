using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    public void InstantiateWeapon()
    {
        if (weapon != null)
            Instantiate(weapon, transform);
    }
}
