using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 14/2/2022
 */

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private GameObject Head, Body, MeleeWeapon, RangedWeapon, Zombie, MainCamera, FollowCamera, LockOnCamera;
    private Cinemachine.CinemachineBrain brain;

    private Rigidbody m_Rigidbody;
    private float horizontal, vertical, targetAngle, angle, distToGround, turnSmoothVelocity;
    private Vector3 direction, moveDir;

    [SerializeField]
    private float turnSmoothTime = 0.1f, maxVelocity = 5f, acceleration = 6f, rotationSpeed = 1;
    private bool IsGrounded, ToRoll = false, LockOn = false;
    private bool[] IsDodging = { false, false };
    private double dodgeTime = 0;
    private Animator playerAnimation;

    // Update is called once per frame
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        playerAnimation = Body.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        distToGround = Body.GetComponent<Collider>().bounds.extents.y;
        brain = MainCamera.GetComponent<Cinemachine.CinemachineBrain>();
    }   

    private void Update()
    {
        IsGrounded = Physics.Raycast(Body.GetComponent<Transform>().position, Vector3.down, distToGround + 0.1f);
        //gravity

        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump") && !ToRoll)
                IsDodging[0] = true;
        }

        if (ToRoll && Input.GetButtonDown("Jump"))
            IsDodging[1] = true;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        dodgeTime -= Time.deltaTime;

        if (Input.GetKeyDown("o"))
        {
            LockOn = !LockOn;
            FollowCamera.SetActive(!FollowCamera.activeSelf);
            LockOnCamera.SetActive(!LockOnCamera.activeSelf);
        }
        if (LockOn)
            transform.LookAt(Zombie.transform.position);
        else
            brain.ManualUpdate();
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            if (IsDodging[0])
            {
                m_Rigidbody.velocity *= 0;
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.forward);
                m_Rigidbody.AddForce((moveDir.normalized * 300) + (Vector3.up.normalized * 100));
                IsDodging[0] = false;
                ToRoll = true;
                dodgeTime = 0.75d;
            }
            else if (IsDodging[1])
            {
                m_Rigidbody.velocity *= 0;
                playerAnimation.Play("Roll");
                moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.forward);
                m_Rigidbody.AddForce(moveDir.normalized * 500 - (Vector3.up.normalized * 100));
                ToRoll = false;
                IsDodging[1] = false;
                dodgeTime = 0.5d;
            }
            else if (IsGrounded)
            {
                if (!LockOn)
                {
                    targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                }
                m_Rigidbody.AddForce((moveDir.normalized * acceleration));
                //controller.Move(moveDir.normalized * acceleration * Time.deltaTime);
            }
        }
        else if (IsDodging[0])
        {
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (-Vector3.forward);
            m_Rigidbody.AddForce((moveDir.normalized * 300) + (Vector3.up.normalized * 150));
            IsDodging[0] = false;
            dodgeTime = 0.5d;
        }
        if (LockOn)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;
            moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);
            brain.ManualUpdate();
        }
        if (dodgeTime <= 0)
        {
            ToRoll = false;
            if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity)
            {
                //smoothness of the slowdown is controlled by the 0.99f, 
                //0.5f is less smooth, 0.9999f is more smooth
                m_Rigidbody.velocity *= 0.8f;
            }
        }
        // Check if player is rolling, disable some colliders
        if (playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            Head.GetComponent<Collider>().enabled = false;
            MeleeWeapon.GetComponent<Collider>().enabled = false;
            RangedWeapon.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Head.GetComponent<Collider>().enabled = true;
            MeleeWeapon.GetComponent<Collider>().enabled = true;
            //RangedWeapon.GetComponent<Collider>().enabled = true;
        }
    }
}
