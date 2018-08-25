using System;
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
    public float MaxWheelTorque = 600f;
    public float BrakeTorque = 12000f;
    public float TopSpeed = 2f;


    public void Accelerate(float amount)
    {
        FrontLeftWheel.brakeTorque = 0f;
        FrontRightWheel.brakeTorque = 0f;

        if (CurrentSpeed() < TopSpeed)
        {
            FrontLeftWheel.motorTorque = amount * MaxWheelTorque;
            FrontRightWheel.motorTorque = amount * MaxWheelTorque;
        } else
        {
            FrontLeftWheel.motorTorque = 0f;
            FrontRightWheel.motorTorque = 0f;
        }
    }

    public float CurrentSpeed()
    {
        return GetComponent<Rigidbody>().velocity.magnitude; 
    }

    public void ApplyBrakes()
    {
        FrontLeftWheel.brakeTorque = BrakeTorque;
        FrontRightWheel.brakeTorque = BrakeTorque;

        FrontLeftWheel.motorTorque = 0f;
        FrontRightWheel.motorTorque = 0f;
    }
}
