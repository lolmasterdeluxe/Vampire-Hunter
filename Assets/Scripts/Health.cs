using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 24/2/2022
 */

public class Health : MonoBehaviour
{
    public float hp;
    [SerializeField]
    private float MaxPoise;
    [SerializeField]
    private int BloodEssenceToDrop;
    [SerializeField]
    private GameObject Pickup;
    [SerializeField]
    private Item Item;
    [HideInInspector]
    public float CurrentPoise;

    private CFSM FSM_Script;
    private void Awake()
    {
        FSM_Script = GetComponent<CFSM>();
        CurrentPoise = MaxPoise;
    }
    private void Update()
    {
        if (CurrentPoise <= 0)
        {
            GetComponent<Animator>().Play("Flinch");
            CurrentPoise = MaxPoise;
        }
        if (hp <= 0 && !GetComponent<Animator>().GetBool("Die"))
        {
            if (gameObject.CompareTag("Player Animation"))
            {
                if (PlayerStats.Health > 0)
                    hp = PlayerStats.Health;    
                else
                {
                    GameObject BloodEssence = Instantiate(Pickup, transform.parent.position, Quaternion.Euler(0,0,0));
                    BloodEssence.GetComponent<PickUp>().bloodEssence = (int)PlayerStats.BloodEssence;
                }
            }
            else
            {
                if (transform.parent.name.Substring(0, 7) == "Vampire")
                {
                    GameObject Key = Instantiate(Pickup, transform.parent);
                    Key.GetComponent<PickUp>().holding = Item;
                }
                GameObject BloodEssence = Instantiate(Pickup, transform.parent);
                BloodEssence.GetComponent<PickUp>().holding = null;
                BloodEssence.GetComponent<PickUp>().bloodEssence = BloodEssenceToDrop;
                GetComponent<Animator>().SetBool("Die", true);
                FSM_Script.enabled = false;
                GetComponentInParent<NavMeshAgent>().enabled = false;
            }
        }
    }

}
