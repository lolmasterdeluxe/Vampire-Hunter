using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

public abstract class Weapon : Item
{
    public abstract void Attack(GameObject target);
    protected void DealDamage(GameObject target, int damage)
    {
        Debug.Log("Deal damage: " + damage + ", to: " + target.name);
    }
}
