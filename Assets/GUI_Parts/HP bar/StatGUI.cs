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
        maxHP = PlayerStats.Vitality;
        currHP = PlayerStats.Health;
    }
        
    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Stamina").GetComponent<PlayerStats>().Stamina -= 10.0f;;
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            currHP+=1;
        }
        progressbar.GetComponent<Image>().fillAmount = (currHP / maxHP);
    }
}
