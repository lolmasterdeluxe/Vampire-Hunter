using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class InventoryUI : MonoBehaviour
{
    public GameObject hud, page;
    public void Display()
    {
        page.SetActive(false);
        hud.SetActive(true);
        hud.GetComponent<InventoryUI_HUD>().UpdateDisplay();
    }

    public void OpenPage()
    {
        page.SetActive(true);
        hud.SetActive(false);
        page.GetComponent<InventoryUI_Page>().UpdateScrollView(0);
    }

    public void ClosePage()
    {
        Display();
    }
}
