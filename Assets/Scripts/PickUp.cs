using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item holding;
    //[SerializeField]
    public GameObject placeholder;
    

    private void Awake()
    {
        if (holding != null && holding.model3D != null)
        {
            Debug.Log("has model");
            Instantiate(holding.model3D, gameObject.transform);
        }
        else
        {
            Debug.Log("no model");
            placeholder.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerInventory.instance.AddItem(holding))
                Destroy(gameObject);
        }
    }
}
