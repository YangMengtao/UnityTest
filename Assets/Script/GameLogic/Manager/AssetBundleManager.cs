using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour
{
    public static readonly string BUNDLE_PATH = 
#if UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IOS
        Application.dataPath + "/Raw/";
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN 
        "file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif

    private IEnumerator LoadAssetBundle(string path)
    {
        WWW bundle = WWW.LoadFromCacheOrDownload(path, 1);
        yield return bundle;
        yield return Instantiate(bundle.assetBundle.mainAsset);
        bundle.assetBundle.Unload(false);
    }

    private IEnumerator LoadAssetBundle(string path, List<string> list)
    {
        WWW bundle = WWW.LoadFromCacheOrDownload(path, 1);
        yield return bundle;
        foreach (string item in list)
        {
            Object obj = bundle.assetBundle.LoadAsset(item);
            yield return Instantiate(obj);
        }
        bundle.assetBundle.Unload(false);
    }
}
