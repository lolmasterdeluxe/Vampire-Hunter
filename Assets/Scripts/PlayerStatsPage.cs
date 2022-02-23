using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsPage : InventoryPage
{
    [SerializeField]
    TextMeshProUGUI playerStats;
    [SerializeField]
    Image[] hotbar = new Image[4];
    [SerializeField]
    Image[] armour = new Image[4];

    public override void UpdatePage(int itemIndex)
    {
        playerStats.text = "ill do this later \n 1 2 3 4 5 \n \n 6 7 8 9 0";

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

        if (PlayerInventory.instance.helmet != null)
        {
            armour[0].sprite = PlayerInventory.instance.helmet.itemImage;
            armour[0].gameObject.SetActive(true);
        }
        else
        {
            armour[0].gameObject.SetActive(false);
        }

        if (PlayerInventory.instance.chestplate != null)
        {
            armour[1].sprite = PlayerInventory.instance.chestplate.itemImage;
            armour[1].gameObject.SetActive(true);
        }
        else
        {
            armour[1].gameObject.SetActive(false);
        }

        if (PlayerInventory.instance.leggings != null)
        {
            armour[2].sprite = PlayerInventory.instance.leggings.itemImage;
            armour[2].gameObject.SetActive(true);
        }
        else
        {
            armour[2].gameObject.SetActive(false);
        }

        if (PlayerInventory.instance.boots != null)
        {
            armour[3].sprite = PlayerInventory.instance.boots.itemImage;
            armour[3].gameObject.SetActive(true);
        }
        else
        {
            armour[3].gameObject.SetActive(false);
        }
    }
}
