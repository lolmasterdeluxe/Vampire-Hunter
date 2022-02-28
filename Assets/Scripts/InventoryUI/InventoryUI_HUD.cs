using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI_HUD : MonoBehaviour
{
    public Image[] hotbar = new Image[1];
    public Image[] armour = new Image[1];

    int selectedSlot = -1;
    public void UpdateDisplay()
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (PlayerInventory.instance.hotbar[i] != null)
            {
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
    public void Select(int i)
    {
        FindObjectOfType<AudioManager>().Play("click");
        //option to equip
        switch (i)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                selectedSlot = i;
                Debug.Log("Change hotbar");
                gameObject.GetComponentInParent<InventoryUI>().OpenPage();
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                selectedSlot = i;
                Debug.Log("Change armour");
                gameObject.GetComponentInParent<InventoryUI>().OpenPage();
                break;
            case 8:
            default:
                Debug.Log("Open Inventory");
                gameObject.GetComponentInParent<InventoryUI>().OpenPage();
                selectedSlot = -1;
                break;
        }
    }
}
