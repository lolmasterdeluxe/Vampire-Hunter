using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class PlayerController : MonoBehaviour
{
    public GameObject Inventory_UI;
    public GameObject MeleeWeapon;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory_UI.SetActive(!Inventory_UI.activeSelf);
            if (Inventory_UI.activeSelf)
            {
                Inventory_UI.GetComponent<InventoryUI>().Display();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            FindAndUseWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FindAndUseWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FindAndUseWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FindAndUseWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FindAndUseWeapon(3);
        }
    }

    private void FindAndUseWeapon(int key)
    {
        int itemID = GetComponent<PlayerInventory>().hotbar[key];
        if (itemID != 0)
        {
            //Weapon weapon = (Weapon)FindObjectOfType<InventorySystem>().items[itemID];
            ((Weapon)FindObjectOfType<InventorySystem>().items[itemID]).Attack(gameObject);
        }
    }
}
