using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    [HideInInspector]
    public bool Hit = false;
    private void WeaponImpact()
    {
        Hit = true;
    }
    private void ExitImpact()
    {
        Hit = false;
    }
}
