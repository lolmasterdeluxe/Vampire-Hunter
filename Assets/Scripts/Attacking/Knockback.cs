using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private GameObject Entity;
    [SerializeField]
    private float KnockbackValue = 150f;
    // Start is called before the first frame update
    private void KnockbackTrue()
    {
        if (gameObject.CompareTag("Enemy Animation"))
        {
            GetComponentInParent<NavMeshAgent>().enabled = false;
            GetComponentInParent<Rigidbody>().isKinematic = false;
        }
        else if (gameObject.CompareTag("Player Animation"))
        {
            GetComponentInParent<PlayerMovement>().enabled = false;
            FindObjectOfType<BasicMeleeWeapon>().enabled = false;
            FindObjectOfType<BasicRangedWeapon>().enabled = false;
            FindObjectOfType<BasicMeleeWeapon>().GetComponent<Animator>().Play("Idle2");
            FindObjectOfType<BasicMeleeWeapon>().GetComponent<Animator>().SetBool("Flinch", true);
            FindObjectOfType<BasicRangedWeapon>().GetComponent<Animator>().Play("Idle2");
        }
        GetComponentInParent<Rigidbody>().AddForce(Quaternion.Euler(0f, Entity.transform.eulerAngles.y, 0f) * (Vector3.forward) * KnockbackValue);
    }

    // Update is called once per frame
    private void KnockbackFalse()
    {
        if (gameObject.CompareTag("Enemy Animation"))
        {
            GetComponentInParent<NavMeshAgent>().enabled = true;
            GetComponentInParent<Rigidbody>().isKinematic = true;
        }
        else if (gameObject.CompareTag("Player Animation"))
        {
            GetComponentInParent<PlayerMovement>().enabled = true;
            FindObjectOfType<BasicMeleeWeapon>().enabled = true;
            FindObjectOfType<BasicRangedWeapon>().enabled = true;
            FindObjectOfType<BasicMeleeWeapon>().GetComponent<Animator>().SetBool("Flinch", false);
        }
    }
}
