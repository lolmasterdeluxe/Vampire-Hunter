using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Li Zeyu
 * Created: 25/2/2022
 */

public class CageInteraction : MonoBehaviour
{
    private bool triggerActive = false;
    [HideInInspector]
    public bool doorOpen = false;
    [SerializeField]
    private GameObject target;

    [SerializeField]
    Key requiredKey;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            if (!doorOpen)
            {
                if (PlayerInventory.instance.RemoveItem(requiredKey))
                {
                    NotificationSystem.instance.Notify(requiredKey.itemName, requiredKey.itemImage, "Item Used");
                    target.GetComponent<Animator>().SetTrigger("OpenDoor");
                    doorOpen = true;
                    TriggerDeath.CagesOpened += 1;
                }
                else
                {
                    NotificationSystem.instance.ShowRequirementPopup();
                }
            }
        }
    }
}
