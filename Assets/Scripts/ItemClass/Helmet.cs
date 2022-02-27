using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 17/2/2022
 */
[CreateAssetMenu(menuName = "Items/New Helmet")]
public class Helmet : Armour
{
    public float requiredDexterity;

    public override bool CheckRequiredStats()
    {
        return (PlayerStats.Dexterity >= requiredDexterity);
    }
}
