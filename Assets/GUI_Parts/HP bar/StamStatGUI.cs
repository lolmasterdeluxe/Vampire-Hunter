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
        PlayerStats.MaxStamina = PlayerStats.Endurance * 10;
        PlayerStats.Stamina = PlayerStats.MaxStamina;
        FindObjectOfType<AudioManager>().Play("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject.Find("Stamina").GetComponent<PlayerStats>().Stamina -= 10.0f;;
        PlayerStats.MaxStamina = PlayerStats.Endurance * 10;


        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerStats.Stamina -= 30;
            //for (int i = 0; i < 30; i++)
            //{
            //    currSTAM--;
            //}
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerStats.Endurance++;
        }


        progressbar.GetComponent<Image>().fillAmount = (PlayerStats.Stamina / PlayerStats.MaxStamina);
        //progressbar.GetComponent<Image>().fillAmount = (currSTAM / maxSTAM);

        PlayerStats.Stamina += (Time.deltaTime * (PlayerStats.Endurance - 7));
    }


}
