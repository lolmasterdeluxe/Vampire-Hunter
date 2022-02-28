using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool PlaySound = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.MaxHealth = PlayerStats.Vitality * 10;
        PlayerStats.Health = PlayerStats.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats.MaxHealth = PlayerStats.Vitality * 10;


        if (PlayerStats.Health > PlayerStats.MaxHealth)
            PlayerStats.Health = PlayerStats.MaxHealth;
        else if (PlayerStats.Health <= 0)
        {
            playdeathsound();
            PlayerStats.Health = 0;
        }
        else
        {
            PlaySound = false; 
        
        }
            
    }
    void playdeathsound()
    {
        if (!PlaySound)
        {
            PlaySound = true;
            FindObjectOfType<AudioManager>().Play("playerdeath");
        }
    }    
}
