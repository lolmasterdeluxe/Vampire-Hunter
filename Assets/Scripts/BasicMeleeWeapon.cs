using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeWeapon : MeleeWeapon
{
    public override void Attack(GameObject target)
    {
        Debug.Log("Melee Weapon Attack.");
        DealDamage(target, itemInfo.damage);
    }
}
