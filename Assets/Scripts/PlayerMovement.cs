using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Rigidbody m_Rigidbody;
    private float horizontal, vertical, targetAngle, angle, distToGround, turnSmoothVelocity;
    public Transform cam;
    private Vector3 direction, moveDir;
    public float turnSmoothTime = 0.1f, maxVelocity = 5f, acceleration = 6f, rollAngle = 0f;
    private bool IsGrounded, ToRoll = false;
    private bool[] IsDodging = { false, false };
    private double dodgeTime = 0;

    // Update is called once per frame
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y; 
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
        //gravity

        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump") && !ToRoll)
                IsDodging[0] = true;
        }

        if (ToRoll && Input.GetButtonDown("Jump"))
            IsDodging[1] = true;


        dodgeTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (IsDodging[0])
            {
                m_Rigidbody.velocity *= 0;
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.forward);
                m_Rigidbody.AddForce((moveDir.normalized * 300) + (Vector3.up.normalized * 150));
                IsDodging[0] = false;
                ToRoll = true;
                dodgeTime = 0.75d;
            }
            else if (IsDodging[1])
            {
                m_Rigidbody.velocity *= 0;  
                moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * (Vector3.forward);
                m_Rigidbody.AddForce((moveDir.normalized * 300 - (Vector3.up.normalized * 150)));
                ToRoll = false;
                IsDodging[1] = false;
                dodgeTime = 0.5d;
            }
            else if (IsGrounded)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                m_Rigidbody.AddForce((moveDir.normalized * acceleration));
                //m_Rigidbody.MovePosition(m_Rigidbody.position + moveDir.normalized * speed * Time.fixedDeltaTime);
                //controller.Move(moveDir.normalized * speed * Time.deltaTime);
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
        if (dodgeTime <= 0)
        {
            ToRoll = false;
            if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity)
            {
                //smoothness of the slowdown is controlled by the 0.99f, 
                //0.5f is less smooth, 0.9999f is more smooth
                m_Rigidbody.velocity *= 0.9f;
            }
        }
    }
}
