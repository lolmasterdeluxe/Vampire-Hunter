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
        if (hp <= 0 && FSM_Script.enabled)
        {
            GameObject BloodEssence = Instantiate(Pickup, transform.parent);
            if (gameObject.CompareTag("Player Animation"))
                BloodEssence.GetComponent<PickUp>().bloodEssence = (int)PlayerStats.BloodEssence;
            else
                BloodEssence.GetComponent<PickUp>().bloodEssence = BloodEssenceToDrop;
            GetComponent<Animator>().SetBool("Die", true);
            FSM_Script.enabled = false;
            GetComponentInParent<NavMeshAgent>().enabled = false;
        }
    }

}
