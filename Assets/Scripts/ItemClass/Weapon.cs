using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

public abstract class Weapon : Usables
{
    public int damage;

    // return true if requirements met
    public abstract bool CheckRequiredStats();
    public void DealDamage(GameObject target, int damage)
    {
        if (target == null || !target.GetComponent<Health>()) return;
        Debug.Log("Deal damage: " + damage + ", to: " + target.name);
        target.GetComponent<Health>().hp -= damage;
    }
}
