using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplayBar : MonoBehaviour
{
    public int itemIndex;
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI stats;

    public void UpdateInfo()
    {
        image.sprite = PlayerInventory.instance.slots[itemIndex].itemImage;
        image.preserveAspect = true;
        itemName.text = PlayerInventory.instance.slots[itemIndex].itemName;
        stats.text = PlayerInventory.instance.slots[itemIndex].PrintShortStats();
    }

    public void SelectSelf()
    {
        FindObjectOfType<InventoryUI_Page>().SelectItem(itemIndex);
    }
}
