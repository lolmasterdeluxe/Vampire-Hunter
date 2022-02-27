using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string scnName)
    {
        Debug.Log("loaded");
        SceneManager.LoadScene(scnName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
