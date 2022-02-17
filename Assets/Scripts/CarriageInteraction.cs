using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageInteraction : MonoBehaviour
{

    private bool triggerActive = false;
    public GameObject text;
    public GameObject panel;
    public GameObject camera;
    public GameObject shop;
    public GameObject travel;
    //public GameObject travelDestination;
    public GameObject player;
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
        camera.SetActive(false);
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
        camera.SetActive(true);
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
