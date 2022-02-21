using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */

public class PlayerInventory : MonoBehaviour
{
    #region Singleton

    public static PlayerInventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerInventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public Item[] slots = new Item[6];
    public Item[] hotbar = new Item[4];
    public Helmet helmet;
    public Chestplate chestplate;
    public Leggings leggings;
    public Boots boots;
}
