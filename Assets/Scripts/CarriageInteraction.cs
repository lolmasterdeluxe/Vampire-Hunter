using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageInteraction : MonoBehaviour
{

    private bool triggerActive = false;
    public GameObject text;
    public GameObject panel;
    public GameObject camera;
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
        else if (triggerActive && panel.activeSelf == false)
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
    }
    public void NoMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
