using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmourStatsPage : InventoryPage
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
    Image armour;

    int selectedItem;
    public override void UpdatePage(int itemIndex)
    {
        selectedItem = itemIndex;
        itemImage.sprite = PlayerInventory.instance.slots[itemIndex].itemImage;
        itemName.text = PlayerInventory.instance.slots[itemIndex].itemName;
        itemDescription.text = PlayerInventory.instance.slots[itemIndex].itemDescription;
        itemStats.text = "ill do this later \n 1 2 3 4 5 \n \n 6 7 8 9 0";

        armour.gameObject.SetActive(false);
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        if (PlayerInventory.instance.slots[itemIndex] is Helmet)
        {
            if (PlayerInventory.instance.helmet != null)
            {
                armour.gameObject.SetActive(true);
                armour.sprite = PlayerInventory.instance.helmet.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
            }
        }
        else if (PlayerInventory.instance.slots[itemIndex] is Chestplate)
        {
            if (PlayerInventory.instance.chestplate != null)
            {
                armour.gameObject.SetActive(true);
                armour.sprite = PlayerInventory.instance.chestplate.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
            }
        }
        else if (PlayerInventory.instance.slots[itemIndex] is Leggings)
        {
            if (PlayerInventory.instance.leggings != null)
            {
                armour.gameObject.SetActive(true);
                armour.sprite = PlayerInventory.instance.leggings.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
            }
        }
        else if (PlayerInventory.instance.slots[itemIndex] is Boots)
        {
            if (PlayerInventory.instance.boots != null)
            {
                armour.gameObject.SetActive(true);
                armour.sprite = PlayerInventory.instance.boots.itemImage;
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
            }
        }
    }

    public void Equip()
    {
        PlayerInventory.instance.EquipArmour(selectedItem);
        transform.parent.parent.GetComponent<InventoryUI_Page>().ReturnToMain();
    }
}
