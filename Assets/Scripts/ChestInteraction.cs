using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Li Zeyu
 * Created: 17/2/2022
 * Editor: Muhammad Rifdi bin Sabbri
 * Edited: 20/2/2022
 */

public class ChestInteraction : MonoBehaviour
{
    private bool triggerActive = false, IsChestOpen = false;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Item Item1;
    [SerializeField]
    private int BloodEssence;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player Animation"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Player Animation"))
        {
            triggerActive = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E) && !IsChestOpen)
        {
            ChestOpen();
        }
    }
    void ChestOpen()
    {
        if (Item1.itemName != ("Empty")) {
            PlayerInventory.instance.AddItem(Item1);
            NotificationSystem.instance.Notify(Item1.itemName, Item1.itemImage, "Item Added");
        }
        if (BloodEssence != 0)
        {
            PlayerStats.BloodEssence += BloodEssence;
        }
        target.GetComponent<Animation>()["ChestAnim"].speed = 1.0f;
        target.GetComponent<Animation>().Play("ChestAnim");
        IsChestOpen = true;
    }
}
