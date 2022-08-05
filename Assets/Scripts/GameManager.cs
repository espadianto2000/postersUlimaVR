using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Globalization;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pelota;


    [Header("API")]
    public TextAsset json;


    [Header("Environment")]
    public GameObject _cube;
    public GameObject _floor;
    public GameObject _roof;
    public GameObject _player;
    public GameObject _stand;
    public GameObject _poster;
    public GameObject _posterSinFoco;
    public GameObject _panel;
    public GameObject _door;
    public GameObject _zoom;
    //añadido version videos----------------------------------------------------<
    public GameObject _video;
    //añadido version videos---------------------------------------------------->
    private Vector3 spawnPosition;
    public GameObject webloader;
    public new GameObject audio;
    

    [Header("Screens")]
    public GameObject loadingScreen;
    public GameObject instructionsScreen;
    public GameObject pauseScreen;
    public GameObject assetBundlePopUp;
    public GameObject ZoomPopUp;
    public GameObject DoorPopUp;

    [Header("Variables")]
    public int numEvent;
    public int currentRoom = 0;
    public GameObject cam;
    private bool seeInstructions;
    private AssetBundle bundle;
    


    private void Start()
    {
        PlayerPrefs.SetInt("AudioState",0);
        StartCoroutine(GenerateScene());
        seeInstructions = true;
    }

    public void ToggleRoom()
    {
        foreach (GameObject g in SceneManager.GetSceneByName("SampleScene").GetRootGameObjects())
        {
            g.SetActive(true);
            if (g.name.Contains("Cube") || g.name.Contains("Floor") || g.name.Contains("poster") ||
                g.name.Contains("Stand") || g.name.Contains("Roof") || g.name.Contains("Door") ||
                g.name.Contains("Panel") || g.tag == "AssetBundle" || g.name.Contains("Video") || g.name.Contains("Zoom"))
            {
                Destroy(g);
            }
        }
        _player.SetActive(false);
        cam.SetActive(false);
        loadingScreen.SetActive(true);
        //instructionsScreen.SetActive(true);
        StartCoroutine(GenerateScene());
        
    }



    IEnumerator GenerateScene()
    {
        audio.SetActive(false);
        JSONNode jsonInfo = JSON.Parse(json.text);
        JSONNode events = jsonInfo["events"][numEvent];
        JSONNode environment = JSON.Parse(events["environment"].ToString());
        string URLbundle = environment[currentRoom]["assetBundle"];
        URLbundle = environment[currentRoom]["assetBundle"];
        
        System.Globalization.CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        culture.NumberFormat.NumberDecimalSeparator = ".";
        foreach (JSONNode data in environment[currentRoom]["walls"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            yield return StartCoroutine(Generate(pos, rot, sca, _cube, color, texture));
        }

        foreach (JSONNode data in environment[currentRoom]["doors"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            int toRoom = data["to"];
            yield return StartCoroutine(Generate(pos, rot, sca, _door, color, texture, toRoom: toRoom));
        }

        foreach (JSONNode data in environment[currentRoom]["panel"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            string text = data["text"];
            string textColor = data["textColor"];
            yield return StartCoroutine(Generate(pos, rot, sca, _panel, color, texture, text, textColor));
        }

        foreach (JSONNode data in environment[currentRoom]["floors"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            yield return StartCoroutine(Generate(pos, rot, sca, _floor, color, texture));
        }

        foreach (JSONNode data in environment[currentRoom]["roof"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            yield return StartCoroutine(Generate(pos, rot, sca, _roof, color, texture));
        }


        foreach (JSONNode data in environment[currentRoom]["player"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            spawnPosition = pos;
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            Thread.Sleep(10);
            _player.transform.position = pos;
            Thread.Sleep(10);
            _player.transform.localRotation = Quaternion.Euler(rot);
            Thread.Sleep(10);
            _player.transform.localScale = sca;
        }

        foreach (JSONNode data in environment[currentRoom]["stand"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));

            GameObject obj = Instantiate(_stand, transform.position, Quaternion.Euler(rot));
            obj.transform.position = pos;
            obj.transform.localScale = sca;


            if (data["info"] != null)
            {
                int index = 1;
                foreach (JSONNode posterStand in data["info"])
                {
                    yield return StartCoroutine(GeneratePoster(posterStand["name"], posterStand["image"],
                            posterStand["pdf"], obj, "stand", index));
                    index++;
                }
            }


        }

        foreach (JSONNode data in environment[currentRoom]["posters"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            if (data["info"]["image"])
            {
                GameObject obj = null;
                if (!data["info"]["foco"])
                {
                    obj = Instantiate(_posterSinFoco, transform.position, Quaternion.Euler(rot));
                    if (data["info"]["color"])
                    {
                        Color matColor;
                        if (ColorUtility.TryParseHtmlString(data["info"]["color"], out matColor))
                        {
                            obj.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", matColor);
                            obj.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", matColor);
                        }
                    }
                }
                else {
                    obj = Instantiate(_poster, transform.position, Quaternion.Euler(rot));
                }
                obj.transform.position = pos;
                obj.transform.localScale = sca;
                yield return StartCoroutine(GeneratePoster(data["info"]["name"], data["info"]["image"], data["info"]["pdf"], obj, "poster"));
            }
        }

        //añadido version videos----------------------------------------------------<
        foreach (JSONNode data in environment[currentRoom]["videos"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string color = data["color"];
            string texture = data["texture"];
            string url = data["url"];
            yield return StartCoroutine(GenerateVideos(pos, rot, sca, _video, color, texture, url));
        }
        //añadido version videos---------------------------------------------------->
        //añadido version zoom--------------------------------------------------->
        foreach (JSONNode data in environment[currentRoom]["zoom"])
        {
            Vector3 pos = new Vector3(float.Parse(data["position"]["x"], culture), float.Parse(data["position"]["y"], culture), float.Parse(data["position"]["z"], culture));
            Vector3 rot = new Vector3(float.Parse(data["rotation"]["x"], culture), float.Parse(data["rotation"]["y"], culture), float.Parse(data["rotation"]["z"], culture));
            Vector3 sca = new Vector3(float.Parse(data["scale"]["x"], culture), float.Parse(data["scale"]["y"], culture), float.Parse(data["scale"]["z"], culture));
            string url = data["url"];
            yield return StartCoroutine(GenerateZoom(pos, rot, sca, url));
        }
        //añadido version zoom--------------------------------------------------->
        string[] arrayModel = new string[99];
        int posModel = 0;
        foreach (JSONNode data in environment[currentRoom]["model"])
        {
            arrayModel[posModel] = data["name"];
            posModel = posModel + 1;
        }



        //StartCoroutine(GetAssetBundle(URLbundle, arrayModel));

            
        
        if (seeInstructions)
        {
            instructionsScreen.SetActive(true);
            seeInstructions = false;
        }
        else
        {
            cam.SetActive(false);
            _player.SetActive(true);
        }
        Instantiate(pelota);
        
    }
    IEnumerator GenerateZoom(Vector3 pos, Vector3 rot, Vector3 sca, string url)
    {
        GameObject obj = Instantiate(_zoom, transform.position, Quaternion.Euler(rot));
        obj.transform.position = pos;
        obj.transform.localScale = sca;
        obj.GetComponentInChildren<Text>().text = url;
        yield return null;
    }

    IEnumerator Generate(Vector3 newPosition, Vector3 rotation, Vector3 scale, GameObject toInstantiate, string color = "", string texture = "", 
        string textString = "", string textColor = "", int toRoom = 0)
    {
        GameObject obj = Instantiate(toInstantiate, transform.position, Quaternion.Euler(rotation));
        obj.transform.position = newPosition;
        obj.transform.localScale = scale;
        if (texture != "" && texture != null)
        {
            yield return StartCoroutine(GetTexture(texture, obj, false));
        }
        if (color != "" && color != null)
        {
            Color matColor;
            if (ColorUtility.TryParseHtmlString(color, out matColor))
            {
                obj.GetComponent<Renderer>().material.SetColor("_Color", matColor);
            }
        }
        if(textString != "" && textString != null)
        {
            GameObject text = obj.transform.GetChild(0).gameObject;
            text.GetComponent<TextMeshPro>().text = textString;
        }
        if(textColor != "" && textColor != null)
        {
            Color textColorAux;
            if(ColorUtility.TryParseHtmlString(textColor, out textColorAux))
            {
                GameObject text = obj.transform.GetChild(0).gameObject;
                text.GetComponent<TextMeshPro>().color = textColorAux;
            }
        }
        if (toInstantiate == _door)
        {
            obj.transform.name = "Door_" + toRoom.ToString();
        }
        yield return null;

    }

    IEnumerator GeneratePoster(string name, string urlTexture, string urlPDF, GameObject obj, string type = "poster", int index = 0, bool local = false)
    {
        Poster posterIns = ScriptableObject.CreateInstance<Poster>();
        posterIns.Init(name, urlPDF);

        switch (type)
        {
            case "stand":
                obj.transform.GetChild(index).gameObject.GetComponent<PosterDisplay>().poster = posterIns;
                GameObject auxObj = obj.transform.GetChild(index).gameObject;
                yield return StartCoroutine(GetTexture(urlTexture, auxObj));
                break;
            default:
                obj.GetComponent<PosterDisplay>().poster = posterIns;
                yield return StartCoroutine(GetTexture(urlTexture, obj));
                break;
        }

        yield return null;
    }

    IEnumerator GetTexture(string URL, GameObject obj, bool poster = true)
    {
        
        UnityWebRequest posterSpriteRequest = UnityWebRequestTexture.GetTexture(URL);
        yield return posterSpriteRequest.SendWebRequest();
        if (posterSpriteRequest.isNetworkError || posterSpriteRequest.isHttpError)
        {
            yield return new WaitForSeconds(2f);
            //SceneManager.UnloadSceneAsync("SampleScene");
            //Screen.orientation = ScreenOrientation.Portrait;
            //foreach (GameObject g in SceneManager.GetSceneByName("LoginScene").GetRootGameObjects())
            //{
            //    g.SetActive(true);
            //    if (g.name.Contains("Cube") || g.name.Contains("Floor") || g.name.Contains("poster") || g.name.Contains("Stand") || g.name.Contains("Roof"))
            //    {
            //        Destroy(g);
            //    }
            //}
            //Debug.LogError(posterSpriteRequest.error);
            yield break;
        }
        else
        {
            if (poster)
            {
                obj.GetComponent<PosterDisplay>().poster.sprite = ((DownloadHandlerTexture)posterSpriteRequest.downloadHandler).texture;
                obj.GetComponent<PosterDisplay>().UpdateValues();
            }
            else
            {
                obj.GetComponent<Renderer>().material.SetTexture("_MainTex", ((DownloadHandlerTexture)posterSpriteRequest.downloadHandler).texture);
                /*Material t = obj.GetComponent<Renderer>().material;
                Vector2 v = new Vector2 (0f,0f);
                v.Set(500f, 500f);
                t.SetTextureScale(1, v);*/
            }
        }
        
    }

    //añadido version videos----------------------------------------------------<
    IEnumerator GenerateVideos(Vector3 newPosition, Vector3 rotation, Vector3 scale, GameObject toInstantiate, string color = "", string texture = "", string url = "")
    {
        GameObject obj = Instantiate(_video);
        obj.transform.position = newPosition;
        obj.transform.rotation = Quaternion.Euler(rotation);
        obj.transform.localScale = scale;
        obj.GetComponent<VideoPlayer>().url = url;
        if (texture != "" && texture != null)
        {
            yield return StartCoroutine(GetTexture(texture, obj, false));
        }
        if (color != "" && color != null)
        {
            Color matColor;
            if (ColorUtility.TryParseHtmlString(color, out matColor))
            {
                obj.GetComponent<Renderer>().material.SetColor("_Color", matColor);
            }
        }
        yield return null;

    }
    //añadido version videos---------------------------------------------------->

    IEnumerator GetAssetBundle(string bundleUrl, string[] arrayModel)
    {
        if (bundleUrl !=null){
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(www);
            }

            int posmodel = 0;

            foreach (string model in arrayModel)
            {
                if (model != null)
                {
                    Thread.Sleep(10); //A veces se presentan errores por cargas
                    webloader.GetComponent<BundleWebLoader>().GetModel(bundle, model);
                    posmodel++;
                }
                else
                {
                    break;
                }

            }
            if (bundle!= null) //Evitar problemas de depender del bundle, si no hay se buguea
                bundle.Unload(false);
        }
        loadingScreen.SetActive(false);
        audio.SetActive(true);
        
    }


}