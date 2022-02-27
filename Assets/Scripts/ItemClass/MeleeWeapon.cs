using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

[CreateAssetMenu(menuName = "Items/New Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public float requiredStrength;

    public override bool CheckRequiredStats()
    {
        return (PlayerStats.Strength >= requiredStrength);
    }
    public override string PrintStats()
    {
        return "Required Strength: " + requiredStrength
            + "\nCurrent Strength: " + PlayerStats.Strength
            + "\nDamage: " + damage;
    }
}
