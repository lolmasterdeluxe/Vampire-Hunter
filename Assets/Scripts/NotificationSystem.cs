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
    [SerializeField]
    GameObject popup;

    public void Start()
    {
        gameObject.SetActive(true);
        notificationPanel.SetActive(false);
        popup.SetActive(false);
    }
    public void Notify(string text, Sprite image, string title)
    {
        notificationPanel.SetActive(true);
        GameObject boxGO = Instantiate(notificationBox, notificationPanel.transform);
        boxGO.GetComponent<NotificationBox>().image.sprite = image;
        boxGO.GetComponent<NotificationBox>().text.text = text;
        boxGO.GetComponent<NotificationBox>().title.text = title;
    }

    public void DestroyNotificationBox(GameObject gameObject)
    {
        Destroy(gameObject);
        if (notificationPanel.transform.childCount <= 1)
        {
            notificationPanel.SetActive(false);
        }
    }

    public void ShowRequirementPopup()
    {
        popup.SetActive(true);
        popup.GetComponent<NotificationPopup>().timer = 1;
        popup.GetComponent<NotificationPopup>().message.text = "Requirements not met";
    }
    public void ShowHealedPopup()
    {
        popup.SetActive(true);
        popup.GetComponent<NotificationPopup>().timer = 1;
        popup.GetComponent<NotificationPopup>().message.text = "Health fully replenished";
    }
}
