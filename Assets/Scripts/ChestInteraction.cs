using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Li Zeyu
 * Created: 17/2/2022
 */

public class ChestInteraction : MonoBehaviour
{
    private bool triggerActive = false;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Item item1;
    [SerializeField]
    private int bloodessence;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            ChestOpen();
            
            //if(item1.itemName != string.Empty)
            //{
            //    Debug.Log("output1");
            //    PlayerInventory.instance.AddItem(item1);
            //    NotificationSystem.instance.Notify(item1.itemName, item1.itemImage, "Item Added");
            //}
            //if (item2.itemName != string.Empty)
            //{
            //    Debug.Log("output2");
            //    PlayerInventory.instance.AddItem(item2);
            //    NotificationSystem.instance.Notify(item2.itemName, item2.itemImage, "Item Added");
            //}

        }
    }
    void ChestOpen()
    {
        if (item1.itemName != ("Empty")) {
            PlayerInventory.instance.AddItem(item1);
            NotificationSystem.instance.Notify(item1.itemName, item1.itemImage, "Item Added");
        }
        if(bloodessence != 0)
        {
            PlayerStats.BloodEssence += bloodessence;
        }
        Debug.Log("output2");
        target.GetComponent<Animation>()["ChestAnim"].speed = 1.0f;
        target.GetComponent<Animation>().Play("ChestAnim");
    }
}
