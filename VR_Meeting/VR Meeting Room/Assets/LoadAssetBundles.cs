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
        using (UnityWebRequest repo = UnityWebRequestAssetBundle.GetAssetBundle("https://github.com/wwm0nkey/VR_Meeting/tree/master/AssetBundles"))
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
