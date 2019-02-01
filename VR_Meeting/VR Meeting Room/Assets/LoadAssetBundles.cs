using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    AssetBundle myLoadedAssetbundle;
    public string path;
    public GameObject clones;
    public Transform spawnLocation;

    void Start()
    {
        StartCoroutine(RetrieveAssetBundle());
    //    InstantiateObjectFromBundle(shapeName, spawnLocation);
    }

    IEnumerator RetrieveAssetBundle()
    {
        using (UnityWebRequest repo = UnityWebRequestAssetBundle.GetAssetBundle("https://www.dropbox.com/sh/loq5l8fvpjsqxjx/AADn2KWQUUvlGk4AobrlL-hsa?dl=0"))
        {
            yield return repo.SendWebRequest();

            if(repo.isNetworkError || repo.isHttpError)
            {
                Debug.Log(repo.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(repo);
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
        var prefab = myLoadedAssetbundle.LoadAsset(assetName);
        Instantiate(prefab, spawnLocation);
    }

}
