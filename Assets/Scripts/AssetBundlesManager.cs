using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    //name of loaded asset bundle
    public string assetBundleName;

    //stores loaded asset bundle from which assets will be used
    private AssetBundle ab;

    //URL asset bundle address
    public string assetBundleURL;


    private void Start()
    {
        if (string.IsNullOrEmpty(assetBundleURL))
        {
            StartCoroutine(LoadAssets());
        }
        else
        {
            StartCoroutine(LoadAssetsFromURL());
        }
    }


    private IEnumerator LoadAssetsFromURL() 
    { 
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("User-Agent", "DefaultBrowser");

        yield return uwr.SendWebRequest();

        //if error occurs error log will be displayed
        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            ab = DownloadHandlerAssetBundle.GetContent(uwr);
        }

        Debug.Log(ab == null ? "Failed to download Asset Bundle" : "Asset bundle downloaded");
    }



    //coroutine asynchronous loading of assets
    private IEnumerator LoadAssets()
    { 
        //object of class AssetBoundleCreateRequest
        AssetBundleCreateRequest abcr;

        //asset bundle path
        string path = Path.Combine(Application.streamingAssetsPath, assetBundleName);

        //loads asynchronous assets
        abcr = AssetBundle.LoadFromFileAsync(path);

        yield return abcr;

        ab = abcr.assetBundle;

        Debug.Log(ab == null ? "Failed to load Asset Bundle" : "Asset Bundle loaded");

    }


    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }

}
