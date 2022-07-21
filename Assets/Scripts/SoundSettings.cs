using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public GameObject music;
    public Slider slider;
    private float maxVolume;


    void Start()
    {
        maxVolume = music.GetComponent<AudioSource>().volume;
    }

    void Update()
    {
        UpdateVolume(slider.value);   
    }


    private void UpdateVolume (float value)
    {
       music.GetComponent<AudioSource>().volume = maxVolume * value;
        
    }
}
