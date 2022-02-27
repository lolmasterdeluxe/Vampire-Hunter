using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StamStatGUI : MonoBehaviour
{
    public GameObject progressbar;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        progressbar.GetComponent<Image>().fillAmount = (PlayerStats.Stamina / PlayerStats.MaxStamina);
    }


}
