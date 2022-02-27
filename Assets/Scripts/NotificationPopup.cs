using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPopup : MonoBehaviour
{
    public float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        gameObject.SetActive(timer > 0);
    }
}
