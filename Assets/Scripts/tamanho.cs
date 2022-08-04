using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamanho : MonoBehaviour
{
    Vector3 escInicial;
    Vector3 posInicial;
    Quaternion rotationI;
    // Start is called before the first frame update
    void Start()
    {
        escInicial = transform.localScale;
        posInicial = transform.position;
        rotationI = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void achicar()
    {
        transform.localScale = new Vector3(transform.localScale.x*0.3f, transform.localScale.y*0.3f,transform.localScale.z*0.3f);
    }
    public void agrandar()
    {
        transform.localScale = escInicial;
        transform.position = posInicial;
        transform.rotation = rotationI;
    }
}
