using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Inventory_UI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory_UI.SetActive(!Inventory_UI.activeSelf);
            if (Inventory_UI.activeSelf) Inventory_UI.GetComponent<InventoryUI>().Display();
        }
    }
}
