using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterDisplay : MonoBehaviour
{
    public Poster poster;

    public void UpdateValues()
    {
        this.transform.name = poster.name;
        gameObject.GetComponent<Renderer>().material.mainTexture = poster.sprite;
    }
}
