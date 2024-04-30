using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical);
        direction = transform.TransformDirection(direction);
        direction = new Vector3(direction.x, 0f, direction.z);
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }
}