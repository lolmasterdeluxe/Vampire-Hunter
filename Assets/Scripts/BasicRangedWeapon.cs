using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class BasicRangedWeapon : RangedWeapon
{
    public override void Attack(GameObject target)
    {
        Debug.Log("Ranged Weapon Attack.");
        DealDamage(target, itemInfo.damage);
    }
}
