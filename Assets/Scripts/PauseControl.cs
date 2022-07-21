using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PauseControl : MonoBehaviour
{
    //Todo es un script nuevo en el branch de videos------------------------------------------------------------------------
    public GameObject Player;
    private int estado;
    public new GameObject audio;
    // Start is called before the first frame update
    void Start()
    {
        
        estado = 0;
        this.GetComponent<VideoPlayer>().Pause();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) {
            
            if (Vector3.Distance(this.transform.position, Player.transform.position) > 4 && estado == 1)
            {
                PlayerPrefs.SetInt("AudioState", PlayerPrefs.GetInt("AudioState") - 1);
                this.GetComponent<VideoPlayer>().Pause();
                estado = 0;
                if (PlayerPrefs.GetInt("AudioState") == 0)
                {
                    audio.GetComponent<AudioSource>().Play();
                }
            }
            else if (Vector3.Distance(this.transform.position, Player.transform.position) < 4 && estado == 0)
            {
                PlayerPrefs.SetInt("AudioState", PlayerPrefs.GetInt("AudioState") + 1);
                audio.GetComponent<AudioSource>().Pause();
                this.GetComponent<VideoPlayer>().Play();
                estado = 1;
            }
        }
        else 
        {
            try
            {
                Player = GameObject.Find("Player");
                audio = Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            }
            catch
            {

            }

        }

    }
}
