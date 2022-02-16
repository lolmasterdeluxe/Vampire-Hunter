using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeWeapon : MeleeWeapon
{
    public Animator playerAnimation;
    public GameObject target;
    bool comboPossible;
    int comboStep;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack(target);
        }
    }
    public override void Attack(GameObject target)
    {
        if (comboStep == 0)
        {
            playerAnimation.Play("Attack1");
            comboStep = 1;
            Debug.Log("Melee Weapon Attack.");
            DealDamage(target, itemInfo.damage);
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }
    public void Combo(GameObject target)
    {
        if (comboStep == 2)
        {
            playerAnimation.Play("Attack2");
            DealDamage(target, itemInfo.damage);
        }
        if (comboStep == 3)
        {
            playerAnimation.Play("Attack3");
            DealDamage(target, itemInfo.damage);
        }
    }
    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
        
}
