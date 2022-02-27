using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StatGUI : MonoBehaviour
{
    public GameObject progressbar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.MaxHealth = PlayerStats.Vitality * 10;
        PlayerStats.Health = PlayerStats.MaxHealth;
    }
        
    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Stamina").GetComponent<PlayerStats>().Stamina -= 10.0f;;
        PlayerStats.MaxHealth = PlayerStats.Vitality * 10;

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerStats.Health -= 30;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerStats.Vitality++;
        }
        progressbar.GetComponent<Image>().fillAmount = (PlayerStats.Health / PlayerStats.MaxHealth);
    }
}
