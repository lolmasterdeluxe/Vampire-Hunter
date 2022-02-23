using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Page : MonoBehaviour
{
    public GameObject displayPanel;
    public GameObject displayBar;

    public GameObject playerStatsPage;
    public GameObject weaponStatsPage;
    public GameObject armourStatsPage;
    public enum TAB_TYPES 
    {
        ALL_TAB
        ,MELEE_TAB
        ,RANGED_TAB
        ,HELMET_TAB
        ,CHESTPALTE_TAB
        ,LEGGINGS_TAB
        ,BOOTS_TAB
    }
    public void ReturnToMain()
    {
        UpdateScrollView(0);
    }
    void ChangePage(GameObject pageGO, int itemIndex = 0)
    {
        armourStatsPage.SetActive(false);
        playerStatsPage.SetActive(false);
        weaponStatsPage.SetActive(false);
        pageGO.SetActive(true);
        pageGO.GetComponent<InventoryPage>().UpdatePage(itemIndex);
    }

    public void SelectItem(int itemIndex)
    {
        if (PlayerInventory.instance.slots[itemIndex] is Weapon)
            ChangePage(weaponStatsPage, itemIndex);
        else if (PlayerInventory.instance.slots[itemIndex] is Armour)
            ChangePage(armourStatsPage, itemIndex);
        else
            ChangePage(playerStatsPage);
    }
    public void GoBack()
    {
        gameObject.GetComponentInParent<InventoryUI>().ClosePage();
    }

    public void UpdateScrollView(int tab)
    {
        UpdateScrollView((TAB_TYPES)tab);
    }
    public void UpdateScrollView(TAB_TYPES tab)
    {
        ChangePage(playerStatsPage);

        foreach (Transform child in displayPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < PlayerInventory.instance.slots.Length; i++)
        {
            if (PlayerInventory.instance.slots[i] == null) continue;
            
            bool toDisplay = false;
            switch(tab)
            {
                case TAB_TYPES.ALL_TAB:
                    toDisplay = true;
                    break;
                case TAB_TYPES.MELEE_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is MeleeWeapon);
                    break;
                case TAB_TYPES.RANGED_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is RangedWeapon);
                    break;
                case TAB_TYPES.HELMET_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is Helmet);
                    break;
                case TAB_TYPES.CHESTPALTE_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is Chestplate);
                    break;
                case TAB_TYPES.LEGGINGS_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is Leggings);
                    break;
                case TAB_TYPES.BOOTS_TAB:
                    toDisplay = (PlayerInventory.instance.slots[i] is Boots);
                    break;
                default:
                    break;
            }
            if (!toDisplay) continue;

            GameObject displayBarObject = Instantiate(displayBar, displayPanel.transform);
            displayBarObject.GetComponent<ItemDisplayBar>().itemIndex = i;
            displayBarObject.GetComponent<ItemDisplayBar>().UpdateInfo();
        }
    }
}
