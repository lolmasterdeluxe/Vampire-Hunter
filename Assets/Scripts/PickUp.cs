using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item holding;
    public int bloodEssence;
    [SerializeField]
    Sprite bloodEssenceSprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            if (holding)
            {
                if (PlayerInventory.instance.AddItem(holding))
                {
                    NotificationSystem.instance.Notify(holding.itemName, holding.itemImage, "Picked Up");
                    Destroy(gameObject);
                }
            }
            else
            {
                PlayerStats.BloodEssence += bloodEssence;
                NotificationSystem.instance.Notify("Blood Essence", bloodEssenceSprite, "Picked Up");
            }
            FindObjectOfType<AudioManager>().Play("itempickup");
        }
    }
}
