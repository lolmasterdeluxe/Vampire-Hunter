using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedWeapon : RangedWeapon
{
    public override void Attack(GameObject target)
    {
        Debug.Log("Ranged Weapon Attack.");
        DealDamage(target, itemInfo.damage);
    }
}
