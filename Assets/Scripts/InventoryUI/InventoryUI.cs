using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class InventoryUI : MonoBehaviour
{
    public GameObject hud, page;

    public Image meleeWeapon;
    public Image rangedWeapon;
    public TextMeshProUGUI bloodEssenceText;

    private void Awake()
    {
        gameObject.SetActive(true);
        meleeWeapon.gameObject.SetActive(true);
        rangedWeapon.gameObject.SetActive(true);
        Hide();
    }

    private void Update()
    {
        // this hurts
        bloodEssenceText.text = PlayerStats.BloodEssence.ToString();
    }

    public void Display()
    {
        page.SetActive(false);
        hud.SetActive(true);
        hud.GetComponent<InventoryUI_HUD>().UpdateDisplay();
    }

    public void Hide()
    {
        page.SetActive(false);
        hud.SetActive(false);
        meleeWeapon.sprite = PlayerInventory.instance.meleeWeapon.itemImage;
        rangedWeapon.sprite = PlayerInventory.instance.rangedWeapon.itemImage;
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
        meleeWeapon.sprite = PlayerInventory.instance.meleeWeapon.itemImage;
        rangedWeapon.sprite = PlayerInventory.instance.rangedWeapon.itemImage;
    }

    public bool IsDisplaying()
    {
        return (hud.activeSelf || page.activeSelf);
    }
}
