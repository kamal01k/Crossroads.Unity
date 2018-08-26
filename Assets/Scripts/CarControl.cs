using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour, ICarControl
{

    [Header("Wheels")]
    public WheelCollider FrontLeftWheel;
    public WheelCollider FrontRightWheel;
    public WheelCollider BackLeftWheel;
    public WheelCollider BackRightWheel;

    [Header("Constants")]
    public float MaxWheelTorque = 800f;
    public float BrakeTorque = 12000f;
    public float TopSpeed = 3f;

    public float MaxSteerAngleDegrees = 45f;

    private float _steerChangeSpeed = 0.3f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public float CurrentSpeed => _rigidbody.velocity.magnitude;


    public void Accelerate(float amount)
    {
        if (amount < -1f || amount > 1f)
            throw new ArgumentException("Argument out of bounds.");

        FrontLeftWheel.brakeTorque = 0f;
        FrontRightWheel.brakeTorque = 0f;


        if (CurrentSpeed < TopSpeed)
        {
            FrontLeftWheel.motorTorque = amount * MaxWheelTorque;
            FrontRightWheel.motorTorque = amount * MaxWheelTorque;
        }
        else
        {
            FrontLeftWheel.motorTorque = 0f;
            FrontRightWheel.motorTorque = 0f;
        }
    }

    public void Brake(float amount)
    {
        if (amount < 0f || amount > 1f)
            throw new ArgumentException("Argument out of bounds.");

        FrontLeftWheel.brakeTorque = amount * BrakeTorque;
        FrontRightWheel.brakeTorque = amount * BrakeTorque;

        FrontLeftWheel.motorTorque = 0f;
        FrontRightWheel.motorTorque = 0f;
    }

    public void Steer(float angle)
    {
        var targetAngle = Mathf.Clamp(angle, -MaxSteerAngleDegrees, MaxSteerAngleDegrees);
        float newAngle = DampAngle(targetAngle);

        FrontLeftWheel.steerAngle = newAngle;
        FrontRightWheel.steerAngle = newAngle;

    }

    private float DampAngle(float targetAngle)
    {
        var oldAngle = FrontLeftWheel.steerAngle;
        var deltaSign = (targetAngle - oldAngle) > 0f ? 1 : -1;
        var newAngle = (Math.Abs(targetAngle - oldAngle) < _steerChangeSpeed) ? targetAngle : oldAngle + deltaSign * _steerChangeSpeed;

        return newAngle;
    }
}

public interface ICarControl
{
    /// <summary>
    /// Accelerate forward or reverse. Kills brakes.
    /// </summary>
    /// <param name="amount">Acceleration amount, between -1 (full reverse) and 1 (full forward)</param>
    void Accelerate(float amount);

    /// <summary>
    /// Apply breaking force. Kills motor torque.
    /// </summary>
    /// <param name="amount">Break strength, between 0 and 1</param>
    void Brake(float amount);

    /// <summary>
    /// Steer towards direction provided by angle relative to forward.
    /// </summary>
    /// <param name="angle">Angle relative to forward direction in degrees.</param>
    void Steer(float angle);

}