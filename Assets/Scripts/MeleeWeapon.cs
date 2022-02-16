using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public abstract class MeleeWeapon : Weapon
{
    public abstract override void Attack(GameObject target);
}
