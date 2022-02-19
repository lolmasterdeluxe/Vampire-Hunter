using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Li Zeyu
 * Created: 15/2/2022
 */

public class CarriageInteraction : MonoBehaviour
{
    private bool triggerActive = false;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject shop;
    [SerializeField]
    private GameObject travel;
    //public GameObject travelDestination;
    [SerializeField]
    private GameObject player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {

        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            ShowPanel();
        }
        else if(triggerActive && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCarriage();
        }
        else if (triggerActive && panel.activeSelf == false && shop.activeSelf == false && travel.activeSelf == false)
        {
            ShowHint();
        }
        else
            HideHint();
    }

    public void ShowHint()
    {
        text.SetActive(true);
    }
    public void HideHint()
    {
        text.SetActive(false);
    }
    public void ShowPanel()
    {
        panel.SetActive(true);
        Camera.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        //GameObject.Find("Player").GetComponent<PlayerMovement>().direction
    }
    public void QuitCarriage()
    {
        panel.SetActive(false);
        shop.SetActive(false);
        travel.SetActive(false);
        Camera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    public void Teleport(int index)
    {
        string target = "Carriage (" + index + ")";
        //Debug.Log("target" + target);
        GameObject travelDestination = GameObject.Find(target);
        Vector3 targetPosition = travelDestination.transform.position;
        player.transform.position = targetPosition + new Vector3(-40,0,3);
        QuitCarriage();
    }
}
