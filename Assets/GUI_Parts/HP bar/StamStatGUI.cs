using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StamStatGUI : MonoBehaviour
{
    public GameObject progressbar;
    public float currSTAM;
    public float maxSTAM;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Stamina").GetComponent<PlayerStats>().Stamina -= 10.0f;;
        PlayerStats.MaxStamina = PlayerStats.Endurance * 10;
        maxSTAM = PlayerStats.MaxStamina;
        currSTAM = PlayerStats.Stamina;
        if (Input.GetKeyDown(KeyCode.L))
        {
            currSTAM += 1;
        }
        progressbar.GetComponent<Image>().fillAmount = (currSTAM / maxSTAM);
        //progressbar.GetComponent<Image>().fillAmount = (currSTAM / maxSTAM);
    }
}
