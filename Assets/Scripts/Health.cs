using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp;
    public float hp;

    private void Start()
    {
        hp = maxHp;
    }
}
