using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

[CreateAssetMenu(menuName = "Items/New Weapon")]
public class WeaponInfo : ItemInfo
{
    public int damage;
    public Animator animator;
}
