using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StatGUI : MonoBehaviour
{
    public GameObject progressbar;
    public float currHP;
    public float maxHP;
    // Start is called before the first frame update
    void Start()
    {

    }
        
    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Stamina").GetComponent<PlayerStats>().Stamina -= 10.0f;;
        PlayerStats.MaxHealth = PlayerStats.Vitality * 10;
        maxHP = PlayerStats.MaxHealth;
        currHP = PlayerStats.Health;

        if (Input.GetKeyDown(KeyCode.K))
        {
            currHP+=1;
        }
        progressbar.GetComponent<Image>().fillAmount = (currHP / maxHP);
    }
}
