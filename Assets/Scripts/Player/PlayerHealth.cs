using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

        if (Input.GetKeyDown(KeyCode.K))
            PlayerStats.Health -= 30;

        if (Input.GetKeyDown(KeyCode.P))
            PlayerStats.Vitality++;

        if (PlayerStats.Health > PlayerStats.MaxHealth)
            PlayerStats.Health = PlayerStats.MaxHealth;
        else if (PlayerStats.Health < 0)
            PlayerStats.Health = 0;
    }
}
