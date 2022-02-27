using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Li Zeyu
 * Created: 17/2/2022
 */

public class ChestInteraction : MonoBehaviour
{
    private bool triggerActive = false;
    [SerializeField]
    private GameObject target;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            ChestOpen();
        }
    }
    void ChestOpen()
    {
        target.GetComponent<Animation>()["ChestAnim"].speed = 1.0f;
        target.GetComponent<Animation>().Play("ChestAnim");
    }
}
