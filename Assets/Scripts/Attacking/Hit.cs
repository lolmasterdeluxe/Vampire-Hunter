using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 25/2/2022
 */

public class Hit : MonoBehaviour
{
    [SerializeField]
    private Impact impact;
    public MeleeWeapon info;
    void OnTriggerStay(Collider Other)
    {
        if (impact.Hit)
        {
            if (gameObject.CompareTag("Player Weapon"))
            {
                if (Other.CompareTag("Enemy Animation"))
                {
                    Other.GetComponent<Animator>().Play("Flinch");
                    info.DealDamage(Other.gameObject, info.damage); 
                    impact.Hit = false;
                }
            }
            else if (gameObject.CompareTag("Enemy Weapon"))
            {
                if (Other.CompareTag("Player Animation"))
                {
                    Other.GetComponent<Animator>().Play("Flinch");
                    info.DealDamage(Other.gameObject, info.damage);
                    impact.Hit = false;
                }
            }
        }
    }


}
