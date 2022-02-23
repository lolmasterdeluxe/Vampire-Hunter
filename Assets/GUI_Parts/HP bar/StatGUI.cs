using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatGUI : MonoBehaviour
{
    public GameObject progressbar;
    public float curr;
    public float max;
    // Start is called before the first frame update
    void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
        progressbar.GetComponent<Image>().fillAmount = curr / max  ;
    }
}
