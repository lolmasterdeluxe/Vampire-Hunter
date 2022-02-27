using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponStatsPage : InventoryPage
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemDescription;
    [SerializeField]
    TextMeshProUGUI itemStats;

    [SerializeField]
    GameObject equipButton;
    [SerializeField]
    Image weapon;

    int selectedItem;
    public override void UpdatePage(int itemIndex)
    {
        selectedItem = itemIndex;
        itemImage.sprite = PlayerInventory.instance.slots[itemIndex].itemImage;
        itemName.text = PlayerInventory.instance.slots[itemIndex].itemName;
        itemDescription.text = PlayerInventory.instance.slots[itemIndex].itemDescription;
        itemStats.text = "";

        weapon.gameObject.SetActive(false);
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        if (PlayerInventory.instance.slots[itemIndex] is MeleeWeapon)
        {
            if (PlayerInventory.instance.meleeWeapon != null)
            {
                weapon.gameObject.SetActive(true);
                weapon.sprite = PlayerInventory.instance.meleeWeapon.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
                itemStats.text = "--Current Weapon--" + "\nDamage: " + PlayerInventory.instance.meleeWeapon.damage;
                itemStats.text += "\n--Selected--\n" + PlayerInventory.instance.meleeWeapon.PrintStats();
            }
        }
        else if (PlayerInventory.instance.slots[itemIndex] is RangedWeapon)
        {
            if (PlayerInventory.instance.rangedWeapon != null)
            {
                weapon.gameObject.SetActive(true);
                weapon.sprite = PlayerInventory.instance.rangedWeapon.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
                itemStats.text = "--Current Weapon--" + "\nDamage: " + PlayerInventory.instance.rangedWeapon.damage;
                itemStats.text += "\n--Selected--\n" + PlayerInventory.instance.rangedWeapon.PrintStats();
            }
        }
    }

    public void Equip()
    {
        if (PlayerInventory.instance.EquipWeapon(selectedItem))
            transform.parent.parent.GetComponent<InventoryUI_Page>().ReturnToMain();
        else
            NotificationSystem.instance.ShowRequirementPopup();
    }
}
