using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{

    [Header("Wheels")]
    public WheelCollider FrontLeftWheel;
    public WheelCollider FrontRightWheel;
    public WheelCollider BackLeftWheel;
    public WheelCollider BackRightWheel;

    [Header("Constants")]
    public float TotalDriveTorque = 600f;
    public float BrakeTorque = 12000f;
    public float TopSpeed = 20f;


    void Start()
    {
    }

    void Update()
    {

        float accelerationInput = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);

        var driveTorque = accelerationInput * TotalDriveTorque;

        var relative = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        Debug.Log(relative);

        FrontLeftWheel.brakeTorque = 0f;
        FrontRightWheel.brakeTorque = 0f;

        FrontLeftWheel.motorTorque = driveTorque;
        FrontRightWheel.motorTorque = driveTorque;
    }

}
