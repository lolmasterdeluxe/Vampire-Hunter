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
    private GameObject text;
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

    private GameObject textGameobject;
    private Text statsText;
    float[] tempstatsArr = new float[10];
    float[] statsArr = { PlayerStats.Vitality, PlayerStats.Endurance, PlayerStats.Strength, PlayerStats.Dexterity
    ,PlayerStats.Defense,PlayerStats.Health,PlayerStats.Stamina,PlayerStats.Weapon1Dmg,PlayerStats.Weapon2Dmg,PlayerStats.BloodEssence};
    string[] textArr = { "Vit", "End", "Str", "Dex", "Def", "HP", "Sta", "W1", "W2" ,"BE"};
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
       
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            ShowPanel();
        }
        else if(triggerActive && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCarriage();
        }
        else if (triggerActive && panel.activeSelf == false && shop.activeSelf == false && travel.activeSelf == false)
        {
            ShowHint();
        }
        else
            HideHint();
    }

    public void ShowHint()
    {
        text.SetActive(true);
    }
    public void HideHint()
    {
        text.SetActive(false);
    }
    public void ShowPanel()
    {
        panel.SetActive(true);
        Camera.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        //GameObject.Find("Player").GetComponent<PlayerMovement>().direction
      
    }
    public void QuitCarriage()
    {
        panel.SetActive(false);
        shop.SetActive(false);
        travel.SetActive(false);
        Camera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    public void Teleport(int index)
    {
        string target = "Carriage (" + index + ")";
        //Debug.Log("target" + target);
        GameObject travelDestination = GameObject.Find(target);
        Vector3 targetPosition = travelDestination.transform.position;
        player.transform.position = targetPosition;
        QuitCarriage();
    }

    public void TAdding(int tInt)
    {
        if(tInt == 10)
        {
            for (int i = 0; i < 9; i++)
            {
                textGameobject = GameObject.Find(textArr[i] + "T");
                statsText = textGameobject.GetComponent<Text>();
                statsText.text = tempstatsArr[i] + "";
            }
            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Thanks.";

        }
        else if ((tempstatsArr[9] - MoneyCost(tempstatsArr[tInt]+1 + statsArr[tInt])) < 0)
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

            tempstatsArr[9] -= MoneyCost(tempstatsArr[tInt] + statsArr[tInt]);
            textGameobject = GameObject.Find("BET");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)tempstatsArr[9] + "";

            textGameobject = GameObject.Find("CarriageText");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = "Thats a nice pick.";

            textGameobject = GameObject.Find(textArr[tInt] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[tInt] + 1 + statsArr[tInt]) + "";
        }
       
      
    }
    public  void ClearAll()
    {
        textGameobject = GameObject.Find("CarriageText");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = "Take your time.";
        for (int i = 0; i < 9; i++)
        {
            tempstatsArr[i] = 0;
            textGameobject = GameObject.Find(textArr[i] + "T");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = tempstatsArr[i] + "";

            textGameobject = GameObject.Find(textArr[i] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[i] + 1 + statsArr[i]) + "";
        }
        tempstatsArr[9] = statsArr[9];
        textGameobject = GameObject.Find("BET");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = (int)tempstatsArr[9] + "";
    }
    public void ConfirmPurchase()
    {
        for (int i = 0; i<9; i++)
        {
            statsArr[i] += tempstatsArr[i];
            tempstatsArr[i] = 0;
        }
        statsArr[9] = tempstatsArr[9];
        tempstatsArr[9] = 0;
        TAdding(10);
        ShowStats();
    }
    public void ShowStats()
    {
        for (int i = 0; i < 9; i++)
        {
            textGameobject = GameObject.Find(textArr[i] + "C");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = statsArr[i] + "";
        }
        for (int i = 0; i < 9; i++)
        {
            textGameobject = GameObject.Find(textArr[i] + "P");
            statsText = textGameobject.GetComponent<Text>();
            statsText.text = (int)MoneyCost(tempstatsArr[i] + 1 + statsArr[i])+"";
        }
       
        tempstatsArr[9] = statsArr[9];
        textGameobject = GameObject.Find("BET");
        statsText = textGameobject.GetComponent<Text>();
        statsText.text = (int)tempstatsArr[9] + "";
    }
    private float MoneyCost(float input)
    {
        float i = Mathf.Pow(0.02f * input, 3) + Mathf.Pow(3.06f * input, 2) + 105.6f * input - 895;
        return i;
    }
}
