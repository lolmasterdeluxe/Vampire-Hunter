using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationBox : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public TextMeshProUGUI title;

    float timer = 3;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            NotificationSystem.instance.DestroyNotificationBox(gameObject);
        }
    }
}
