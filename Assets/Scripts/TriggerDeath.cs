using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TriggerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Image img;
    [SerializeField]
    private Image winimg;
    [SerializeField]
    private GameObject trigger;
    [SerializeField]
    private GameObject wintrigger;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject hpbar;
    [SerializeField]
    private GameObject Title;
    [SerializeField]
    private InventoryUI Inventory_UI;

    private bool faded = false;
    private bool won = false;
    public static int lastCarriage = 0;
    public static int CagesOpened = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CagesOpened = 10;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerStats.Health = 0;
        }
        if (!won)
        {
            if (CagesOpened >= 10)
            {
                hpbar.SetActive(false);
                Camera.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ///trigger.SetActive(true);
                wintrigger.SetActive(true);
                if (faded == false)
                {
                    StartCoroutine(FadeWinImage());
                    faded = true;
                }

            }
            else if (PlayerStats.Health <= 0)
            {
                hpbar.SetActive(false);
                Camera.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                
                player.GetComponentInChildren<Animator>().SetBool("Die", true);
                player.GetComponent<InventoryController>().enabled = false;
                Inventory_UI.GetComponent<InventoryUI>().Hide();
                player.GetComponentInChildren<Collider>().enabled = false;

                trigger.SetActive(true);
                if (faded == false)
                {
                    StartCoroutine(FadeImage());
                    faded = true;
                }
            }
        }
        
    }

    IEnumerator FadeImage()
    {
        
        for(float i=0; i <= 1; i += (Time.deltaTime*0.25f))
        {
            img.color = new Color(0,0,0, i);
            yield return null;
        }
        
    }
    IEnumerator FadeWinImage()
    {

        for (float i = 0; i <= 1; i += (Time.deltaTime * 0.5f))
        {
            winimg.color = new Color(0.717f, 0.670f, 0.077f, i);
            yield return null;
        }

    }

    public void Respawn()
    {
        faded = false;

        trigger.SetActive(false);
        string target = "Carriage (" + (lastCarriage+1) + ")";
        GameObject travelDestination = GameObject.Find(target);
        Vector3 targetPosition = travelDestination.transform.position + new Vector3(0, 0, 5);
        player.transform.position = targetPosition;

        PlayerStats.Health = PlayerStats.MaxHealth;

        hpbar.SetActive(true);
        Camera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player.GetComponentInChildren<Animator>().SetBool("Die", false);
        player.GetComponent<InventoryController>().enabled = true;
        player.GetComponentInChildren<Collider>().enabled = true;
        Debug.Log("Player name: " + player.GetComponentInChildren<Animator>().name);
    }
    public void Quitgame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void FreeRoam()
    {
        Title.SetActive(true);
        won = true;
        faded = false;
        hpbar.SetActive(true);
        Camera.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ///trigger.SetActive(true);
        wintrigger.SetActive(false);
    }
}
