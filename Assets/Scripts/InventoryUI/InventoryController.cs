using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class InventoryController : MonoBehaviour
{
    public GameObject Inventory_UI;

    private void Awake()
    {
        //Inventory_UI = FindObjectOfType<InventoryUI>().gameObject;
        Inventory_UI.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("pressed I");
            if (!Inventory_UI.GetComponent<InventoryUI>().IsDisplaying())
            {
                Inventory_UI.GetComponent<InventoryUI>().Display();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Inventory_UI.GetComponent<InventoryUI>().Hide();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else if (Inventory_UI.GetComponent<InventoryUI>().IsDisplaying())
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<CameraLockOn>().enabled = false;
            gameObject.GetComponentInChildren<BasicMeleeWeapon>().enabled = false;
            gameObject.GetComponentInChildren<BasicRangedWeapon>().enabled = false;
        }
        else
        {
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
}
