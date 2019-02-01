﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    AssetBundle myLoadedAssetbundle;
    public string path;
    public GameObject clones;
    public Transform spawnLocation;
    private bool isLoaded = false;
    AssetBundle bundle;

    void Start()
    {
        // StartCoroutine(RetrieveAssetBundle());
        //    InstantiateObjectFromBundle(shapeName, spawnLocation);
    }

    IEnumerator RetrieveAssetBundle(string objectName)
    {
        using (UnityWebRequest repo = UnityWebRequestAssetBundle.GetAssetBundle("https://www.dropbox.com/s/ixld0l0oblu6060/shapes.unity3d?dl=1"))
        {
            yield return repo.SendWebRequest();

            if (repo.isNetworkError || repo.isHttpError)
            {
                Debug.Log(repo.error);
            }
            else
            {
                if (!isLoaded)
                {
                    bundle = DownloadHandlerAssetBundle.GetContent(repo);
                    var prefab = bundle.LoadAsset(objectName);
                    Instantiate(prefab, spawnLocation);
                    isLoaded = true;
                }
                else
                {
                    var prefab = bundle.LoadAsset(objectName);
                    Instantiate(prefab, spawnLocation);
                }
            }
        }
    }

    //void LoadAssetBundle(string bundleUrl)
    //{
    //    myLoadedAssetbundle = AssetBundle.LoadFromFile(bundleUrl);
    //    Debug.Log(myLoadedAssetbundle == null ? "Failed to load AssetBundle" : "AssetBundle successfully loaded");
    //}



    public void InstantiateObjectFromBundle(string assetName)
    {
        Destroy(GameObject.FindGameObjectWithTag("Shape"));
        Debug.Log(assetName);
        StartCoroutine(RetrieveAssetBundle(assetName));
        // var prefab = myLoadedAssetbundle.LoadAsset(assetName);
        // Instantiate(prefab, spawnLocation);
    }

}
