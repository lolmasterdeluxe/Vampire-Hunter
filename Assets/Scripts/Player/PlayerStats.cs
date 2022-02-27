using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 20/2/2022
 */



public static class PlayerStats
{
    // Vitality increases player max hp
    public static float Vitality = 12;

    // Endurance increases player max stamina
    public static float Endurance = 12;

    // Strength is required to equip axes and chestplates
    public static float Strength = 12;

    // Dexterity is required to equip guns, helmets, leggings and shoes
    public static float Dexterity = 12;

    // Defense is the combined defense values of all of player's equipped armour
    public static float Defense = 12;
    public static float MaxHealth;
    public static float MaxStamina;
    public static float Weapon1Dmg = 12;
    public static float Weapon2Dmg = 12;
    public static float BloodEssence = 50000;

    public static float Health;
    public static float Stamina;
}
