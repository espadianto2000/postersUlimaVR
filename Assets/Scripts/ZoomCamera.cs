using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public Camera cam;
    private int bandera;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            cam.fieldOfView = 26;
        }
        else
        {
            cam.fieldOfView = 60;
        }
    }
}

