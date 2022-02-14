using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerControls : MonoBehaviour
{
    // Update is called once per frame
    Rigidbody rb;

    float camXrot = 0;
    float camYrot = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseXmovement = Input.GetAxis("Mouse X");
            float mouseYmovement = Input.GetAxis("Mouse Y");
            camXrot += mouseXmovement;
            camYrot -= mouseYmovement;
        }

        camYrot = Mathf.Clamp(camYrot, -90f, 90f);

        Camera.main.transform.rotation = Quaternion.Euler(camYrot, camXrot, 0);

        Camera.main.transform.position = transform.position - Camera.main.transform.forward * 5f;
    }
    void FixedUpdate()
    {
        if (Input.GetKey("up"))
        {
            rb.MovePosition(rb.position + Vector3.forward * .05f);
        }
        if (Input.GetKey("down"))
        {
            rb.MovePosition(rb.position + Vector3.back * .05f);
        }
        if (Input.GetKey("left"))
        {
            rb.MovePosition(rb.position + Vector3.left * .05f);
        }
        if (Input.GetKey("right"))
        {
            rb.MovePosition(rb.position + Vector3.right * .05f);
        }
    }
}
