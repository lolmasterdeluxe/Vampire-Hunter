using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Author: Li Zeyu
 * Created: 15/2/2022
 */

public class CarriageInteraction : MonoBehaviour
{
    private bool triggerActive = false;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject shop;
    [SerializeField]
    private GameObject travel;
    //public GameObject travelDestination;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int number;
    [SerializeField]
    private GameObject hpbar;

    private bool stopped = false;
    private GameObject textGameobject;
    private Text statsText;
    bool[] carriageUnlocked = { CarriageStats.carriage1, CarriageStats.carriage2};
    float[] tempstatsArr = new float[7];
    float[] statsArr = { PlayerStats.Vitality, PlayerStats.Endurance, PlayerStats.Strength, PlayerStats.Dexterity
    ,PlayerStats.BloodEssence,PlayerStats.MaxHealth,PlayerStats.MaxStamina};
    string[] textArr = { "Vit", "End", "Str", "Dex" ,"BE","MH","MS"};
    
    public void OnTriggerEnter(Collider other)
    {
        if ((other.transform.parent && other.transform.parent.CompareTag("Player")))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((other.transform.parent && other.transform.parent.CompareTag("Player")))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (stopped)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<CameraLockOn>().enabled = false;
            player.GetComponentInChildren<BasicMeleeWeapon>().enabled = false;
            player.GetComponentInChildren<BasicRangedWeapon>().enabled = false;
        }


        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            ShowPanel();
            Debug.Log("open");       
            stopped = true;
        }
        else if (triggerActive && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCarriage();
        }
        else
            return;
        //else if (triggerActive && panel.activeSelf == false && shop.activeSelf == false && travel.activeSelf == false)
        //{
        //    ShowHint();
        //}
        //else
        //    HideHint();
    }

    //public void ShowHint()
    //{
    //    text.SetActive(true);
    //}
    //public void HideHint()
    //{
    //    text.SetActive(false);
    //}
    public void ShowPanel()
    {
        hpbar.SetActive(false);
        panel.SetActive(true);
        Camera.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (carriageUnlocked[number - 1] == false)
        {
            carriageUnlocked[number - 1] = true;
        }
        else {
            
            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Hey hunter, what do you want.";
        }
        PlayerStats.Health = PlayerStats.MaxHealth;

    }
    public void QuitCarriage()
    {
        hpbar.SetActive(true);
        stopped = false;
        panel.SetActive(false);
        shop.SetActive(false);
        travel.SetActive(false);
        Camera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Teleport(int index)
    {
        string target = "Carriage (" + index + ")";
        //Debug.Log("target" + target);
        GameObject travelDestination = GameObject.Find(target);


        if (carriageUnlocked[index-1] == true)
        {
            Vector3 targetPosition = travelDestination.transform.position + new Vector3(5, 0, 0);
            player.transform.position = targetPosition;
            QuitCarriage();
        }
        else
        {
            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Location locked";
        }
        
    }

    public void TAdding(int tInt)
    {
        if(tInt == 10)
        {
            for (int i = 0; i < 4; i++)
            {
                textGameobject = GameObject.Find(textArr[i] + "T");
                statsText = textGameobject.GetComponent<Text>();
                statsText.text = tempstatsArr[i] + "";
            }
            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Thanks.";

        }
        else if ((tempstatsArr[4] - MoneyCost(tempstatsArr[tInt]+1 + statsArr[tInt])) < 0)
        {
            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Hey, thats not enough.";
        }
        else
        {
            tempstatsArr[tInt] += 1;
            textGameobject = GameObject.Find(textArr[tInt] + "T");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = tempstatsArr[tInt] + "";

            tempstatsArr[4] -= MoneyCost(tempstatsArr[tInt] + statsArr[tInt]);
            textGameobject = GameObject.Find("BET");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)tempstatsArr[4] + "";

            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Thats a nice pick.";

            textGameobject = GameObject.Find(textArr[tInt] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[tInt] + 1 + statsArr[tInt]) + "";
            PrintHpSta();
        }
       
      
    }
    public  void ClearAll()
    {
        textGameobject = GameObject.Find("CarriageText");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = "Take your time.";
        for (int i = 0; i < 4; i++)
        {
            tempstatsArr[i] = 0;
            textGameobject = GameObject.Find(textArr[i] + "T");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = tempstatsArr[i] + "";

            textGameobject = GameObject.Find(textArr[i] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[i] + 1 + statsArr[i]) + "";
        }
        tempstatsArr[4] = statsArr[4];
        textGameobject = GameObject.Find("BET");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = (int)tempstatsArr[4] + "";
        PrintHpSta();
    }
    public void ConfirmPurchase()
    {
        for (int i = 0; i<4; i++)
        {
            statsArr[i] += tempstatsArr[i];
            tempstatsArr[i] = 0;
        }
        statsArr[4] = tempstatsArr[4];
        tempstatsArr[4] = 0;
        TAdding(10);
        ShowStats();
    }
    public void ShowStats()
    {
        for (int i = 0; i < 4; i++)
        {
            textGameobject = GameObject.Find(textArr[i] + "C");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = statsArr[i] + "";
            textGameobject = GameObject.Find(textArr[i] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[i] + 1 + statsArr[i]) + "";
        }

        PrintHpSta();
        tempstatsArr[4] = statsArr[4];
        textGameobject = GameObject.Find("BET");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = (int)tempstatsArr[4] + "";
    }
    private float MoneyCost(float input)
    {
        float i = Mathf.Pow(0.02f * input, 3) + Mathf.Pow(3.06f * input, 2) + 105.6f * input - 895;
        return i;
    }

    private void PrintHpSta()
    {

        textGameobject = GameObject.Find("MHT");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = "Max Health: " + (tempstatsArr[0] + statsArr[0]) * 10;

        textGameobject = GameObject.Find("MST");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = "Max Stamina: "+(tempstatsArr[1] + statsArr[1]) * 10 ;
        
    }
    
}
