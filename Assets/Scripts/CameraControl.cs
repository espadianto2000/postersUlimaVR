using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public GameManager manager;
    private GameObject player;
    private float speed_Original;
    public new GameObject audio;
    private bool isPaused = false;



    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
             OpenNewTab(url);
    }

    private void Start()
    {
        LockCursor();
        player = GameObject.Find("Player");
        speed_Original = player.GetComponent<MovementPlayer>().speed;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                pauseScene();
            }
            else
            {
                exitPause();
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.8f))
        {
            if (hit.transform.CompareTag("Door") && !isPaused)
            {
                manager.DoorPopUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0) && !isPaused)
                {
                    GameObject door = hit.transform.gameObject;
                    string name = door.transform.name;
                    string[] toRoom = name.Split('_');
                    manager.currentRoom = int.Parse(toRoom[toRoom.Length - 1]);
                    manager.DoorPopUp.SetActive(false);
                    manager.ToggleRoom();
                }
            }
            

            if (hit.transform.CompareTag("AssetBundle") && !isPaused)
            {
                manager.assetBundlePopUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0) && !isPaused)
                {
                    string URL = "https://prueba-diego-aeb80.web.app/html/";
                    GameObject assetbundle = hit.transform.gameObject;
                    string name = assetbundle.transform.name;
                    string[] obj = name.Split('(');
                    URL = URL + obj[0] + ".html";
                    try
                    {
                        openIt(URL);
                    }
                    catch
                    {
                        Debug.Log("No es un navegador");
                    }
                    pauseScene();
                }
            }

            if (hit.transform.CompareTag("Zoom") && !isPaused) 
            {
                manager.ZoomPopUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return) && !isPaused)
                {
                    GameObject iconoZoom = hit.transform.gameObject;
                    string url = iconoZoom.GetComponentInChildren<Text>().text;
                    try
                    {
                        openIt(url);
                    }
                    catch
                    {
                        Debug.Log("No es un navegador");
                    }
                    pauseScene();
                }
                    
            }

        }
        else
        {
            manager.assetBundlePopUp.SetActive(false);
            manager.ZoomPopUp.SetActive(false);
            manager.DoorPopUp.SetActive(false);

        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void exitPause()
    {
        LockCursor();
        isPaused = false;
        manager.pauseScreen.SetActive(false);
        mouseSensitivity = 100f;
        player.GetComponent<MovementPlayer>().speed = speed_Original;
        audio.GetComponent<AudioSource>().Play();
    }

    public void pauseScene()
    {
        UnlockCursor();
        isPaused = true;
        manager.ZoomPopUp.SetActive(false);
        manager.assetBundlePopUp.SetActive(false);
        manager.pauseScreen.SetActive(true);
        mouseSensitivity = 0;
        player.GetComponent<MovementPlayer>().speed = 0;
        audio.GetComponent<AudioSource>().Pause();
    }


}