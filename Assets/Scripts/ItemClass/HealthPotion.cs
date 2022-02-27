using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Health Potion")]
public class HealthPotion : Potion
{
    public float hp;
    public override void Use()
    {
        Debug.Log("Heal the player for " + hp + " hp.");
    }

    public override string PrintStats()
    {
        return "hp: +" + hp;
    }
    public override string PrintShortStats()
    {
        return "+" + hp + " hp";
    }
}
