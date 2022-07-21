using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleWebLoader : MonoBehaviour
{
    private string assetName;
    private AssetBundle bundle;

    //void Start()
    //{
    //    GetModel(bundle, assetName);
    //}

    public void GetModel(AssetBundle bundle, string assetName)
    {
        if (bundle != null) //Evitar problemas de depender del bundle, si no hay se buguea
            Instantiate(bundle.LoadAsset(assetName)); //Este es un objeto del paquete
    }
}
