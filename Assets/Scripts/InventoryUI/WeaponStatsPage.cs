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
    GameObject emptyButton;
    [SerializeField]
    GameObject equipButton;
    [SerializeField]
    Image[] hotbar = new Image[4];

    public GameObject selectFrame;

    int selectedHotbarSlot = -1;
    int selectedItem = -1;
    public override void UpdatePage(int itemIndex)
    {
        selectedHotbarSlot = -1;
        selectFrame.SetActive(false);

        emptyButton.SetActive(true);
        equipButton.SetActive(false);

        selectedItem = itemIndex;
        itemImage.sprite = PlayerInventory.instance.slots[itemIndex].itemImage;
        itemName.text = PlayerInventory.instance.slots[itemIndex].itemName;
        itemDescription.text = PlayerInventory.instance.slots[itemIndex].itemDescription;

        for (int i = 0; i < PlayerInventory.instance.hotbar.Length; i++)
        {
            if (PlayerInventory.instance.hotbar[i] == null)
                hotbar[i].gameObject.SetActive(false);
            else
            {
                hotbar[i].gameObject.SetActive(true);
                hotbar[i].sprite = PlayerInventory.instance.hotbar[i].itemImage;
            }
        }

        itemStats.text = "ill do this later \n 1 2 3 4 5 \n \n 6 7 8 9 0";
    }
    public void SelectHotbarSlot(int slot)
    {
        selectedHotbarSlot = slot;
        selectFrame.SetActive(true);
        selectFrame.transform.position = hotbar[slot].transform.position;

        emptyButton.SetActive(false);
        equipButton.SetActive(true);
        if (PlayerInventory.instance.hotbar[slot] == null)
        {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
        else
        {
            equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Replace";
        }
    }
    public void Equip()
    {
        if (PlayerInventory.instance.EquipToHotbar(selectedItem, selectedHotbarSlot))
            transform.parent.parent.GetComponent<InventoryUI_Page>().ReturnToMain();
        else
            NotificationSystem.instance.ShowRequirementPopup();
    }
}
