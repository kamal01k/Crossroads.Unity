using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    private CarControl control;

    private void Start()
    {
        control = GetComponent<CarControl>();
    }

    private void Update()
    {
        control.Accelerate(0.5f);
    }
}
