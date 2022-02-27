using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationPopup : MonoBehaviour
{
    public TextMeshProUGUI message;
    public float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        gameObject.SetActive(timer > 0);
    }
}
