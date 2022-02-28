using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

public abstract class Weapon : Item
{
    public int damage;

    // return true if requirements met
    public abstract bool CheckRequiredStats();
    public void DealDamage(GameObject target, int damage)
    {
        if (target == null || !target.GetComponent<Health>()) return;
        Debug.Log("Deal damage: " + damage + ", to: " + target.name);
        Debug.Log("HP of " + target.name + ": " + target.GetComponent<Health>().hp);

        if (target.CompareTag("Player Animation"))
        {
            int DamageTaken = (int)(damage / (1 + (PlayerStats.Defense/100)));
            PlayerStats.Health -= DamageTaken;
        }
        target.GetComponent<Health>().hp -= damage;
        target.GetComponent<Health>().CurrentPoise -= damage;

    }
    public override string PrintShortStats()
    {
        return "Dmg: " + damage;
    }
}
