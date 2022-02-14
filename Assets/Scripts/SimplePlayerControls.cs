using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerControls : MonoBehaviour
{
    // Update is called once per frame
    Rigidbody rb;
    Camera camera;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }
    private void Update()
    {
        camera.transform.position = rb.position + Vector3.back * 5;
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
