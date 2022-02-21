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
    //InventorySystem inventorySystem;

    public Image[] hotbar = new Image[1];
    public Image[] armour = new Image[1];

    private void Start()
    {
        //inventorySystem = FindObjectOfType<InventorySystem>();
    }
    public void Display()
    {
        //GameObject player = FindObjectOfType<PlayerInventory>().gameObject;
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (PlayerInventory.instance.hotbar[i] != null)
            {
                //hotbar[i].sprite = inventorySystem.items[player.GetComponent<PlayerInventory>().hotbar[i]].itemInfo.itemImage;
                hotbar[i].sprite = PlayerInventory.instance.hotbar[i].itemImage;
                hotbar[i].gameObject.SetActive(true);
            }
            else
            {
                hotbar[i].gameObject.SetActive(false);
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

    public void Select(GameObject target)
    {
        //option to equip

    }

}
