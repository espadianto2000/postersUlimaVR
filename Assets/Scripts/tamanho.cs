using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamanho : MonoBehaviour
{
    Vector3 escInicial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        escInicial = transform.localScale;
    }
    public void achicar()
    {
        transform.localScale = new Vector3(transform.localScale.x*0.3f, transform.localScale.y*0.3f,transform.localScale.z*0.3f);
    }
    public void agrandar()
    {
        transform.localScale = escInicial;
    }
}
