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
    public Usables[] hotbar = new Usables[4];
    public Helmet helmet;
    public Chestplate chestplate;
    public Leggings leggings;
    public Boots boots;

    public Armour GetArmourSlot(int index)
    {
        switch (index)
        {
            case 0:
                return helmet;
            case 1:
                return chestplate;
            case 2:
                return leggings;
            case 3:
                return boots;
            default:
                return null;
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool EquipToHotbar(int slotIndex, int hotbarIndex)
    {
        if (slots[slotIndex] is Usables)
        {
            Item temp = hotbar[hotbarIndex];
            hotbar[hotbarIndex] = slots[slotIndex] as Usables;
            slots[slotIndex] = temp;
            return true;
        }
        return false;
    }
    
    public bool EquipArmour(int slotIndex)
    {
        if (slots[slotIndex] is Helmet)
        {
            Item temp = helmet;
            helmet = (Helmet)slots[slotIndex];
            slots[slotIndex] = temp;
            return true;
        }
        else if (slots[slotIndex] is Chestplate)
        {
            Item temp = chestplate;
            chestplate = (Chestplate)slots[slotIndex];
            slots[slotIndex] = temp;
            return true;
        }
        else if (slots[slotIndex] is Leggings)
        {
            Item temp = leggings;
            leggings = (Leggings)slots[slotIndex];
            slots[slotIndex] = temp;
            return true;
        }
        else if (slots[slotIndex] is Boots)
        {
            Item temp = boots;
            boots = (Boots)slots[slotIndex];
            slots[slotIndex] = temp;
            return true;
        }

        return false;
    }

    public bool UnequipFromHotbar(int hotbarIndex)
    {
        if (AddItem(hotbar[hotbarIndex]))
        {
            hotbar[hotbarIndex] = null;
            return true;
        }
        return false;
    }

    public bool UnequipArmour(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
                if (AddItem(helmet))
                {
                    helmet = null;
                    return true;
                }
                return false;
            case 1:
                if (AddItem(chestplate))
                {
                    chestplate = null;
                    return true;
                }
                return false;
            case 2:
                if (AddItem(leggings))
                {
                    leggings = null;
                    return true;
                }
                return false;
            case 3:
                if (AddItem(boots))
                {
                    boots = null;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
}
