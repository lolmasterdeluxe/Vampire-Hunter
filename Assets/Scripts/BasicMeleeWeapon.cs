using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri
 * Created: 15/2/2022
 * Editor: Ho Junliang 
 * Edited: 16/2/2022
 */
public class BasicMeleeWeapon : MonoBehaviour
{
    public MeleeWeapon info;
 
    [SerializeField]
    private Animator playerAnimation;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject Parent;
    [SerializeField]
    private GameObject RangedWeaponArm;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private CameraLockOn playerCamera;
    [SerializeField]
    private float turnSmoothTime = 0.1f;

    private Vector3 lungeDir, direction;
    private float horizontal, vertical, angle, targetAngle, turnSmoothVelocity;

    private bool comboPossible, lunge = false, changeDir = false;
    private int comboStep;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            Attack(target); 
            RangedWeaponArm.GetComponent<BasicRangedWeapon>().enabled = false;
            Parent.GetComponent<PlayerMovement>().enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if (lunge)
        {
            lungeDir = Quaternion.Euler(0f, Parent.GetComponent<Transform>().eulerAngles.y, 0f) * (Vector3.forward);
            Parent.GetComponent<Rigidbody>().AddForce((lungeDir.normalized * 200));
            lunge = false;
            playerCamera.TargetLockOn = false;
        }
        if (!playerCamera.LockOn)
        {
            if (direction.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
                if (changeDir)
                {
                    angle = Mathf.SmoothDampAngle(Parent.GetComponent<Transform>().eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    Parent.GetComponent<Transform>().rotation = Quaternion.Euler(0f, angle, 0f);
                }
            }
        }
        else if (!lunge)
        {
            playerCamera.TargetLockOn = true;
        }
    }
    public void Attack(GameObject target)
    {
        if (comboStep == 0)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack1");
            comboStep = 1;
            Debug.Log("Melee Weapon Attack.");
            //WeaponInfo weaponInfo = (WeaponInfo)itemInfo;
            //DealDamage(target, weaponInfo.damage);
            info.DealDamage(target, info.damage);
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
        changeDir = true;
    }
    public void Combo(GameObject target)
    {
        if (comboStep == 2)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack2");
            info.DealDamage(target, info.damage);
        }
        if (comboStep == 3)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack3");
            info.DealDamage(target, info.damage);
        }
        if (comboStep == 4)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack4");
            info.DealDamage(target, info.damage);
        }
        if (comboStep == 5)
        {
            Parent.GetComponent<Rigidbody>().velocity *= 0;
            playerAnimation.Play("Attack5");
            info.DealDamage(target, info.damage);
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
        Parent.GetComponent<PlayerMovement>().enabled = true;
        RangedWeaponArm.GetComponent<BasicRangedWeapon>().enabled = true;
    }

    public void Lunge()
    {
        lunge = true;
        changeDir = false;
    }

}
