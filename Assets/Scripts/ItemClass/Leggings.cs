using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 17/2/2022
 */
[CreateAssetMenu(menuName = "Items/New Leggings")]
public class Leggings : Armour
{
    public float requiredDexterity;

    public override bool CheckRequiredStats()
    {
        return (PlayerStats.Dexterity >= requiredDexterity);
    }
    public override string PrintStats()
    {
        return "Required Dexterity: " + requiredDexterity
            + "\nCurrent Dexterity: " + PlayerStats.Dexterity
            + "\nDefence: +" + armourPoints;
    }
}
