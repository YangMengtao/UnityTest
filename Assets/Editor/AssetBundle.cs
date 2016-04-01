using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public  class AssetBundle : MonoBehaviour
{
    public enum ePlatform
    {
        eWindows,
        eAndroid,
        eIOS
    }

    private static string m_strSavePath = Application.dataPath + "/StreamingAssets/";
    private static string m_strIOSPath = m_strSavePath + "/IOS/";
    private static string m_strAndroidPath = m_strSavePath + "/Android/";
    private static string m_strWindowsPath = m_strSavePath + "/Windows/";

    [MenuItem ("AssetBundle/New Build AssetBundle")]
    static void CreateAssetBundleNew()
    {
        if (!Directory.Exists("Assets/AssetBundles"))
        {
            Directory.CreateDirectory("Assets/AssetBundles");
        }
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles");
        AssetDatabase.Refresh();
    }

    #region Single Prefab Bundle

    [MenuItem ("AssetBundle/Create Prefab For Editor")]
    static void CreatePrefabAssetBundleForEditor()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eWindows);

        foreach (Object obj in SelectAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);
            if (sourcePath.IndexOf(".prefab") <= 0)
            {
                continue;
            }

            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            targetPath += obj.name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies))
            {
                Debug.Log("IOS : " + obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log("IOS : " + obj.name + "资源打包失败");
            }
        }

        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Create Prefab For IOS")]
    static void CreatePrefabAssetBundleForIOS()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eIOS);

        foreach (Object obj in SelectAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);

            if (sourcePath.IndexOf(".prefab") <= 0)
            {
                continue;
            }

            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            targetPath += obj.name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iOS))
            {
                Debug.Log("IOS : " + obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log("IOS : " + obj.name + "资源打包失败");
            }
        }

        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Create Prefab For Android")]
    static void CreatePrefabAssetBundleForAndroid()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eAndroid);

        foreach (Object obj in SelectAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);

            if (sourcePath.IndexOf(".prefab") <= 0)
            {
                continue;
            }

            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            targetPath += obj.name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android))
            {
                Debug.Log("Android : " + obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log("Android : " + obj.name + "资源打包失败");
            }
        }

        //刷新编辑器
        AssetDatabase.Refresh();
    }

    #endregion

    #region All Prefab AssetBundle

    [MenuItem("AssetBundle/Create All Prefab For Editor")]
    static void CreateAllForEditor()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eWindows) + "allPrefab.assetbundle";

        if (BuildPipeline.BuildAssetBundle(null, SelectAsset, targetPath, BuildAssetBundleOptions.CollectDependencies))
        {
            Debug.Log("Windows : 资源打包成功");
        }
        else
        {
            Debug.Log("Windows : 资源打包失败");
        }

        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Create All Prefab For Android")]
    static void CreateAllForAndroid()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eAndroid) + "allPrefab.assetbundle";

        if (BuildPipeline.BuildAssetBundle(null, SelectAsset, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android))
        {
            Debug.Log("Android : 资源打包成功");
        }
        else
        {
            Debug.Log("Android : 资源打包失败");
        }

        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Create All Prefab For IOS")]
    static void CreateAllForIOS()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        string targetPath = CheckIsHaveDirectory(ePlatform.eIOS) + "allPrefab.assetbundle";

        if (BuildPipeline.BuildAssetBundle(null, SelectAsset, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iOS))
        {
            Debug.Log("IOS : 资源打包成功");
        }
        else
        {
            Debug.Log("IOS : 资源打包失败");
        }

        AssetDatabase.Refresh();
    }
    #endregion

    /// <summary>
    /// 检查路径是否存在，若不存在就创建
    /// </summary>
    /// <param name="platform">平台</param>
    /// <returns>路径</returns>
    private static string CheckIsHaveDirectory(ePlatform platform)
    {
        string targetPath = "";
        switch (platform)
        {
            case ePlatform.eWindows:
                {
                    targetPath = m_strWindowsPath;
                    break;
                }
            case ePlatform.eAndroid:
                {
                    targetPath = m_strAndroidPath;
                    break;
                }
            case ePlatform.eIOS:
                {
                    targetPath = m_strIOSPath;
                    break;
                }
        }

        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }

        return targetPath;
    }

    [MenuItem ("AssetBundle/Clean")]
    static void CleanAssetBundle()
    {
        if (Directory.Exists(m_strSavePath))
        {
            Directory.Delete(m_strSavePath, true);
        }

        if (Directory.Exists("Assets/AssetBundles"))
        {
            Directory.Delete("Assets/AssetBundles", true);
        }

        AssetDatabase.Refresh();
    }
}
