using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOrientation : MonoBehaviour {

    public WheelCollider TargetWheel;

    Vector3 WheelPosition = new Vector3();
    Quaternion WheelRotation = new Quaternion();

    void Update () {
        TargetWheel.GetWorldPose(out WheelPosition, out WheelRotation);
        transform.position = WheelPosition;
        transform.rotation = WheelRotation;
    }
}
