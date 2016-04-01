using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class AssetBundleName : MonoBehaviour
{
    [MenuItem("AssetBundle/Create AssetBundleName")]
    public static void OnCreateAssetBundleName()
    {
        Object[] SelectAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);
            if (sourcePath.LastIndexOf(".") >= 0)
            {
                string path = sourcePath.Substring(0, sourcePath.LastIndexOf("."));
                path += ".ab";
//                 Debug.Log(path);
//                 AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);
                string fileName = Path.GetFileName(sourcePath);
                AssetImporter ai = AssetImporter.GetAtPath(sourcePath);
                if (ai != null)
                {
                    ai.assetBundleName = "ui/" + fileName.Substring(0, fileName.LastIndexOf(".")) + ".ab";
                    Debug.Log(ai.assetBundleName);
                }
            }
         }

        AssetDatabase.RemoveUnusedAssetBundleNames();

        AssetDatabase.Refresh();
    }

    [MenuItem ("Y/All Asset Name")]
    static void AllAssetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
        {
            Debug.Log(name);
        }
    }
}
