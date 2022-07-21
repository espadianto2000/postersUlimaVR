using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{

    public CharacterController controller;
    public float speed;
    //public float rotationSpeed;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.SimpleMove(move * speed * Time.deltaTime);
    }
}