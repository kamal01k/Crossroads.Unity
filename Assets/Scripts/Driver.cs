﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public List<Transform> Waypoints;

    private ICarControl _control;

    private ISteeringBehavour _steeringBehavour;
    private ISpeedBehavour _speedBehavour;

    public Driver()
    {
        _control = new CarControl();
    }

    private void Start()
    {
        _control = GetComponent<CarControl>();
        _steeringBehavour = new FollowWaypoints(Waypoints.ToArray());
        _speedBehavour = new SimpleAccelerate();
    }

    private void FixedUpdate()
    {
        _steeringBehavour.Steer(_control, transform);
        _speedBehavour.RegulateSpeed(_control);
    }
}

public interface ISteeringBehavour
{
    void Steer(ICarControl control, Transform transform);
}

public interface ISpeedBehavour
{
    void RegulateSpeed(ICarControl control);
}

public class FollowWaypoints : ISteeringBehavour
{
    private Transform[] _waypoints;
    private int currentWaypointIndex;

    public FollowWaypoints(Transform[] waypoints)
    {
        _waypoints = waypoints;
    }

    public void Steer(ICarControl control, Transform transform)
    {
        UpdateWaypoint(transform.position);
        float angle = CalculateSteerAngle(transform);
        control.Steer(angle);
    }

    private void UpdateWaypoint(Vector3 currentPosition)
    {
        if (!LastWaypoint() && AtTargetWaypoint(currentPosition))
        {
            currentWaypointIndex++;
        }
    }

    private bool AtTargetWaypoint(Vector3 currentPosition)
    {
        return Vector3.Distance(currentPosition, _waypoints[currentWaypointIndex].position) < 2f;
    }

    private bool LastWaypoint()
    {
        return currentWaypointIndex == _waypoints.Length - 1;
    }

    private float CalculateSteerAngle(Transform transform)
    {
        // gets the vector from object to waypoint with respect to object rotation 
        Vector3 localWaypointVector = transform.InverseTransformPoint(_waypoints[currentWaypointIndex].position);

        // gets the angle toward the waypoint relative to forward (x) axis in degrees 
        float angle = Mathf.Atan2(localWaypointVector.x, localWaypointVector.z) * Mathf.Rad2Deg;

        return angle; 
    }
}



public class SimpleAccelerate : ISpeedBehavour
{
    public void RegulateSpeed(ICarControl control)
    {
        control.Accelerate(0.5f);
    }
}