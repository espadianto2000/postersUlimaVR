
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /// <summary>
    public CharacterController controller;
    
    /// </summary>
    public GameObject cam;
    public GameObject jugador;
    public int speed;
    private bool crouching;
    public float cambioPorSegundo;
    public float altura;
    public float previo;
    public bool agachado;
    // Start is called before the first frame update 
    void Start()
    {
        altura = 2.9f;
        previo = 0;
        agachado = false;
    }

    // Update is called once per frame 
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.SimpleMove(new Vector3(0,0,0));

        previo = jugador.GetComponent<CharacterController>().transform.position.y - jugador.GetComponent<CharacterController>().height / 2 - jugador.GetComponent<CharacterController>().skinWidth;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(agachado==false)
            {
                agachado = true;
                altura = 0.85f;
            }
            else
            {
                agachado = false;
                altura = 2.9f;
            }
        }
        jugador.GetComponent<CharacterController>().height = Mathf.Lerp(jugador.GetComponent<CharacterController>().height, altura, 5f * Time.deltaTime);
        jugador.GetComponent<CharacterController>().transform.position = Vector3.Lerp(jugador.GetComponent<CharacterController>().transform.position, new Vector3(jugador.GetComponent<CharacterController>().transform.position.x, previo + altura / 2 + jugador.GetComponent<CharacterController>().skinWidth, jugador.GetComponent<CharacterController>().transform.position.z), 5f * Time.deltaTime);
        //  m_CharacterController.transform.position = Vector3.Lerp(m_CharacterController.transform.position, new Vector3(m_CharacterController.transform.position.x, previous_y + target_height / 2 + m_CharacterController.skinWidth, m_CharacterController.transform.position.z), 5f * Time.deltaTime);

        //m_CharacterController.height = Mathf.Lerp(m_CharacterController.height, target_height, 5f * Time.deltaTime);




        /*
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            if (crouching && Input.GetKeyDown(KeyCode.E))
            {
                crouching = false;
            }
            else if(!crouching && Input.GetKeyDown(KeyCode.Q))
            {
                crouching = true;
            }
        //}
        if (crouching)
        {
            //cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(cam.transform.position.x, jugador.transform.position.y - 0.9f, cam.transform.position.z), Time.deltaTime * speed);
            if (jugador.GetComponent<CharacterController>().height > 1.5f)
            {

                jugador.GetComponent<CharacterController>().height += -cambioPorSegundo * Time.deltaTime;
            }
            else
            {
                jugador.GetComponent<CharacterController>().height = 1.5f;
            }

        }
        else
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(cam.transform.position.x, jugador.transform.position.y, cam.transform.position.z), Time.deltaTime * speed);
            if (jugador.GetComponent<CharacterController>().height < 3f)
            {
                jugador.GetComponent<CharacterController>().height += cambioPorSegundo * Time.deltaTime;
            }
            else
            {
                jugador.GetComponent<CharacterController>().height = 3f;
            }

        }*/

    }




}

