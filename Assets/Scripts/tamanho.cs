using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamanho : MonoBehaviour
{
    Vector3 escInicial;
    Vector3 posInicial;
    Quaternion rotationI;
    public GameObject foco;
    public bool agarrado;
    public Color colorI;
    // Start is called before the first frame update
    void Start()
    {
        escInicial = transform.localScale;
        posInicial = transform.position;
        rotationI = transform.rotation;
        colorI = transform.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,posInicial)>2.5f)
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            if (agarrado)
            {
                transform.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        transform.GetComponent<Renderer>().material.color = colorI;


    }
    public void achicar()
    {
        agarrado = true;
        if(transform.localScale==escInicial)
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.3f, transform.localScale.y * 0.3f, transform.localScale.z * 0.3f);
            
        }
        foco.SetActive(false);
    }
    public void agrandar()
    {
        agarrado = false;
        //transform.localScale = escInicial;
        //transform.position = posInicial;
        //transform.rotation = rotationI;
        //foco.SetActive(true);
        if(Vector3.Distance(transform.position, posInicial)<=2.5f)
        {
            transform.position = posInicial;
            transform.rotation = rotationI;
            transform.localScale = escInicial;
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            foco.SetActive(true);
        }
    }
}
