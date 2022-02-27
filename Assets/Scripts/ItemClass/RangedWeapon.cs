using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

[CreateAssetMenu(menuName = "Items/New Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public float requiredDexterity;
    public float range;

    public override bool CheckRequiredStats()
    {
        return (PlayerStats.Dexterity >= requiredDexterity);
    }
    public override string PrintStats()
    {
        return "Required Dexterity: " + requiredDexterity
            + "\nCurrent Dexterity: " + PlayerStats.Dexterity
            + "\nDamage: " + damage;
    }
}
