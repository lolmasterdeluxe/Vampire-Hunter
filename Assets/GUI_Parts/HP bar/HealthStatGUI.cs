using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatGUI : MonoBehaviour
{
    public GameObject progressbar;

    // Update is called once per frame
    void Update()
    {
        progressbar.GetComponent<Image>().fillAmount = (PlayerStats.Health / PlayerStats.MaxHealth);
    }

}
