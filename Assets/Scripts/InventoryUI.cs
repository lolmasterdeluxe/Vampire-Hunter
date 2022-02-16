using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class InventoryUI : MonoBehaviour
{
    InventorySystem inventorySystem;

    public Image[] hotbar = new Image[1];
    public Image[] inv = new Image[1];

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }
    public void Display()
    {
        GameObject player = FindObjectOfType<PlayerInventory>().gameObject;
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (player.GetComponent<PlayerInventory>().hotbar[i] != 0)
            {
                hotbar[i].sprite = inventorySystem.items[player.GetComponent<PlayerInventory>().hotbar[i]].itemInfo.itemImage;
                hotbar[i].gameObject.SetActive(true);
            }
            else
            {
                hotbar[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < inv.Length; i++)
        {
            if (player.GetComponent<PlayerInventory>().slots[i] != 0)
            {
                inv[i].sprite = inventorySystem.items[player.GetComponent<PlayerInventory>().slots[i]].itemInfo.itemImage;
                inv[i].gameObject.SetActive(true);
            }
            else
            {
                inv[i].gameObject.SetActive(false);
            }
        }
    }

    public void Select(GameObject target)
    {
        //option to equip

    }

}
