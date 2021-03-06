using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 17/2/2022
 */
public abstract class Armour : Item
{
    public int armourPoints;

    // return true if requirements met
    public abstract bool CheckRequiredStats();

    public override string PrintShortStats()
    {
        return "Def: " + armourPoints;
    }
}
