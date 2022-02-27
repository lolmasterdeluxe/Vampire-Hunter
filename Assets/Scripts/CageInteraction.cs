using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageInteraction : MonoBehaviour
{
    private bool triggerActive = false;
    private bool doorOpen = false;
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
    // Start is called before the first frame update
    void Start()
    {
       
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
                }
                else
                {
                    NotificationSystem.instance.ShowRequirementPopup();
                }
            }
        }
    }
}
