using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Rigidbody m_Rigidbody;
    private Collider collider;
    private float horizontal, vertical, targetAngle, angle;
    public Transform cam;
    Vector3 direction, moveDir, jumpDir;
    public float gravity = -9.81f;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float distToGround;
    private bool IsGrounded, IsDodging = false;

    // Update is called once per frame
    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        distToGround = collider.bounds.extents.y;
    }
    private void Update()
    {
        IsGrounded = Physics.Raycast(m_Rigidbody.position, Vector3.down, distToGround + 0.1f);
        if (Input.GetButtonDown("Jump") && IsGrounded)
            IsDodging = true;
    }
    private void FixedUpdate()
    {
        //gravity
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            m_Rigidbody.AddForce(moveDir.normalized * speed);

            if (IsDodging)
            {
                transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
                moveDir = (-cam.forward);
                jumpDir = Vector3.up;
                m_Rigidbody.AddForce((moveDir.normalized * 250) + (jumpDir.normalized * 50));
                IsDodging = false;
            }
        }
        else if (IsDodging)
        {
            transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
            moveDir = (-cam.forward);
            jumpDir = Vector3.up;
            m_Rigidbody.AddForce((moveDir.normalized * 250) + (jumpDir.normalized * 50));
            IsDodging = false;
        }
        //controller.Move(moveDir.normalized * speed * Time.deltaTime);
        //m_Rigidbody.MovePosition(m_Rigidbody.position + (moveDir.normalized * speed * Time.deltaTime))



    }
}
