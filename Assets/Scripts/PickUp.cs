using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item holding;    

    //private void Awake()
    //{
    //    if (holding != null && holding.model3D != null)
    //    {
    //        Debug.Log("has model");
    //        Instantiate(holding.model3D, gameObject.transform);
    //    }
    //    else
    //    {
    //        Debug.Log("no model");
    //        placeholder.SetActive(true);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            if (PlayerInventory.instance.AddItem(holding))
            {
                NotificationSystem.instance.Notify(holding.itemName, holding.itemImage, "Picked Up");
                Destroy(gameObject);
            }
        }
    }
}
