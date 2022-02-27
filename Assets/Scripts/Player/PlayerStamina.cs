using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.MaxStamina = PlayerStats.Endurance * 10;
        PlayerStats.Stamina = PlayerStats.MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats.MaxStamina = PlayerStats.Endurance * 10;

        if (Input.GetKeyDown(KeyCode.K))
            PlayerStats.Stamina -= 30;

        if (Input.GetKeyDown(KeyCode.P))
            PlayerStats.Endurance++;

        PlayerStats.Stamina += (Time.deltaTime * (PlayerStats.Endurance));

        if (PlayerStats.Stamina > PlayerStats.MaxStamina)
            PlayerStats.Stamina = PlayerStats.MaxStamina;
        else if (PlayerStats.Stamina < 0)
            PlayerStats.Stamina = 0;
    }
}
