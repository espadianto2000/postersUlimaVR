using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    public GameObject manoI;
    public GameObject manoD;
    // Start is called before the first frame update
    void Start()
    {
        manoI = GameObject.Find("Player").transform.GetChild(0).GetChild(1).GetChild(1).gameObject;
        manoD = GameObject.Find("Player").transform.GetChild(0).GetChild(2).GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ocultar()
    {
        manoI.SetActive(false);
        manoD.SetActive(false);
    }
    public void mostrar()
    {
        manoI.SetActive(true);
        manoD.SetActive(true);
    }
}
