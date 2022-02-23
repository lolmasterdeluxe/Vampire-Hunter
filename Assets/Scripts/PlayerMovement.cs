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
    private GameObject Head, Body, MeleeWeapon, RangedWeapon, MainCamera;

    private BasicMeleeWeapon MeleeScript;
    private BasicRangedWeapon RangedScript;
    private CameraLockOn playerCamera;

    private Rigidbody m_Rigidbody;
    private float horizontal, vertical, targetAngle, angle, distToGround, turnSmoothVelocity;
    private Vector3 direction, moveDir;

    [SerializeField]
    private float turnSmoothTime = 0.1f, maxVelocity = 5f, acceleration = 6f;
    private bool IsGrounded, ToRoll = false;
    private bool[] IsDodging = { false, false, false, false };
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
        playerCamera = GetComponent<CameraLockOn>();
        MeleeScript = MeleeWeapon.GetComponent<BasicMeleeWeapon>();
        RangedScript = RangedWeapon.GetComponent<BasicRangedWeapon>();
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(Body.GetComponent<Transform>().position, Vector3.down, distToGround + 0.1f);
        //gravity

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (IsGrounded && !ToRoll)
        {
            if (playerCamera.LockOn)
            {
                if (Input.GetButtonDown("Jump") && horizontal >= 1)
                    IsDodging[2] = true;
                else if (Input.GetButtonDown("Jump") && horizontal <= -1)
                    IsDodging[3] = true;
                else if (Input.GetButtonDown("Jump") && (vertical >= 1 || vertical <= 0))
                    IsDodging[0] = true;
            }
            else if (Input.GetButtonDown("Jump") && (vertical >= 1 || vertical <= 0))
                IsDodging[0] = true;
        }
        Debug.Log("DodgeTime: " + dodgeTime);
        if (ToRoll && Input.GetButtonDown("Jump"))
            IsDodging[1] = true;

        dodgeTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Movement();

        if (dodgeTime <= 0)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            ToRoll = false;

            if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity)
            {
                m_Rigidbody.velocity *= 0.8f;
            }
            Debug.Log("ToRoll bool: " + ToRoll);
            if (playerCamera.LockOn)
                playerCamera.TargetLockOn = true;
        }
        // Check if player is rolling, disable some colliders
        if (playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("Roll") || playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("RollSideRight") || playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("RollSideLeft"))
        {
            Head.GetComponent<Collider>().enabled = false;
            MeleeScript.enabled = false;
            RangedScript.enabled = false;
        }
        else
        {
            Head.GetComponent<Collider>().enabled = true;
            MeleeScript.enabled = true;
            RangedScript.enabled = true;
        }
    }

    private void Movement()
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
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.transform.eulerAngles.y;
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if (!playerCamera.LockOn)
                {
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                m_Rigidbody.AddForce((moveDir.normalized * acceleration));
                //controller.Move(moveDir.normalized * acceleration * Time.deltaTime);
            }
            if (playerCamera.LockOn)
            {
                if (IsDodging[2])
                {
                    m_Rigidbody.velocity *= 0;
                    playerAnimation.Play("RollSideRight");
                    playerCamera.TargetLockOn = false;
                    moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.right);
                    m_Rigidbody.AddForce(moveDir.normalized * 250);
                    ToRoll = true;
                    dodgeTime = 0.5d;
                    IsDodging[2] = false;
                }
                else if (IsDodging[3])
                {
                    m_Rigidbody.velocity *= 0;
                    playerAnimation.Play("RollSideLeft");
                    playerCamera.TargetLockOn = false;
                    moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.left);
                    m_Rigidbody.AddForce(moveDir.normalized * 250);
                    ToRoll = true;
                    dodgeTime = 0.5d;
                    IsDodging[3] = false;
                }
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
        
    }
}