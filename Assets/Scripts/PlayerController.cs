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

    private void Awake()
    {
        //Inventory_UI = FindObjectOfType<InventoryUI>().gameObject;
        Inventory_UI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("pressed I");
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerInventory.instance.UseHotbar(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerInventory.instance.UseHotbar(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerInventory.instance.UseHotbar(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerInventory.instance.UseHotbar(3);
        }
    }
}
