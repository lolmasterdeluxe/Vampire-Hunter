using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private Impact impact;
    void OnTriggerStay(Collider Other)
    {
        if (impact.Hit)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (Other.CompareTag("Enemy Animation"))
                {
                    Other.GetComponent<Animator>().Play("Flinch");
                    impact.Hit = false;
                }
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                if (Other.CompareTag("Player Animation"))
                {
                    Other.GetComponent<Animator>().Play("Flinch");
                    impact.Hit = false;
                }
            }
        }
    }


}
