using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 * Edited: Muhammad Rifdi bin Sabbri
 */
public class BasicMeleeWeapon : MeleeWeapon
{
    [SerializeField]
    private Animator playerAnimation;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject Parent;
    private Vector3 moveDir;

    bool comboPossible, lunge = false;
    int comboStep;

    private void Update()
    {
        moveDir = Quaternion.Euler(0f, Parent.GetComponent<Transform>().eulerAngles.y, 0f) * (Vector3.forward);
        if (Input.GetMouseButtonDown(0))
        {
            Attack(target);
        }
    }
    private void FixedUpdate()
    {
        if (lunge)
        {
            Parent.GetComponent<Rigidbody>().AddForce((moveDir.normalized * 200));
            lunge = false;
        }
    }
    public override void Attack(GameObject target)
    {
        if (comboStep == 0)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack1");
            comboStep = 1;
            Debug.Log("Melee Weapon Attack.");
            WeaponInfo weaponInfo = (WeaponInfo)itemInfo;
            DealDamage(target, weaponInfo.damage);
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
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack2");
            WeaponInfo weaponInfo = (WeaponInfo)itemInfo;
            DealDamage(target, weaponInfo.damage);
        }
        if (comboStep == 3)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack3");
            WeaponInfo weaponInfo = (WeaponInfo)itemInfo;
            DealDamage(target, weaponInfo.damage);
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }

    public void Lunge()
    {
        lunge = true;
    }

}
