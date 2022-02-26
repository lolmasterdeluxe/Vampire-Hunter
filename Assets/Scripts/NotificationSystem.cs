using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    #region Singleton

    public static NotificationSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerInventory found!");
            return;
        }
        instance = this;
    }

    #endregion
    [SerializeField]
    GameObject notificationPanel;
    [SerializeField]
    GameObject notificationBox;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Notify(string text, Sprite image)
    {
        gameObject.SetActive(true);
        GameObject boxGO = Instantiate(notificationBox, notificationPanel.transform);
        boxGO.GetComponent<NotificationBox>().image.sprite = image;
        boxGO.GetComponent<NotificationBox>().text.text = text;
    }

    public void DestroyNotificationBox(GameObject gameObject)
    {
        Destroy(gameObject);
        if (notificationPanel.transform.childCount <= 2)
        {
            this.gameObject.SetActive(false);
        }
    }
}
