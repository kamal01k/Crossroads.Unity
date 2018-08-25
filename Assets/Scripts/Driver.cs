using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    private CarControl control;

    private int counter = 0;

    private void Start()
    {
        control = GetComponent<CarControl>();
    }

    private void Update()
    {
        if (counter++ < 500) 
            control.Accelerate(0.5f);
        else 
            control.ApplyBrakes(0.5f);

    }
}
