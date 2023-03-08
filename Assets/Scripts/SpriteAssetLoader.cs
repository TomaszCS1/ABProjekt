 using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteAssetLoader : Singleton<SpriteAssetLoader>
{
    public string spriteName;

    private AssetBundle ab;

    private IEnumerator Start()
    {
        
            AssetBundleCreateRequest abcr;

            string path = Path.Combine(Application.streamingAssetsPath, spriteName);

            abcr = AssetBundle.LoadFromFileAsync(path);

            yield return abcr;

            ab = abcr.assetBundle;

        
    }


    private IEnumerator LoadAssetOnHduButton()
    {
        {
            AssetBundleCreateRequest abcr;

            string path = Path.Combine(Application.streamingAssetsPath, spriteName);

            abcr = AssetBundle.LoadFromFileAsync(path);

            yield return abcr;

            ab = abcr.assetBundle;

        }

    }



    public Sprite GetSprite( string spriteName)
    {
        return ab.LoadAsset<Sprite>(spriteName);
    }

   

}
