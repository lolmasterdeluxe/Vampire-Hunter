using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public abstract void Attack(GameObject target);
    protected void DealDamage(GameObject target, int damage)
    {
        Debug.Log("Deal damage: " + damage + ", to: " + target.name);
    }
}
