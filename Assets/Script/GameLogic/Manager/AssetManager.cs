using UnityEngine;
using System.Collections;
using System;
//using Game.FrameWork.InterFaces;

public class AssetManager : MonoBehaviour
{
    private UnityEngine.Object LocalLoad(string strPath, Type type = null)
    {
        UnityEngine.Object res = null;

        if (null == type)
        {
            res = Resources.Load(strPath);
        }
        else
        {
            res = Resources.Load(strPath, type);
        }

        if (null == res)
        {
            Debug.LogError("Load resource Failed :" + strPath);
            return null;
        }

        if (res is GameObject && null == type)
        {
            return Instantiate(res);
        }
        else
        {
            return res;
        }
    }

    private void ResLoad(string strPath, Type type, Action<UnityEngine.Object> action)
    {
        StartCoroutine(_ResLoad(strPath, type, action));
    }

    private IEnumerator _ResLoad(string strPath, Type type, Action<UnityEngine.Object> action)
    {
        action(LocalLoad(strPath, type));
        yield break;
    }

    /// <summary>
    /// 加载本地资源
    /// </summary>
    /// <param name="tsParent">加载到哪个节点下面</param>
    /// <param name="strPath">资源路径</param>
    /// <returns></returns>
    public GameObject AddChildByLocal(Transform tsParent, string strPath)
    {
        return AddChildByLocal(tsParent, strPath, Vector3.zero);
    }

    /// <summary>
    /// 加载本地资源
    /// </summary>
    /// <param name="tsParent">加载到哪个节点下面</param>
    /// <param name="strPath">资源路径</param>
    /// <param name="vPos">初始位置</param>
    /// <returns></returns>
    public GameObject AddChildByLocal(Transform tsParent, string strPath, Vector3 vPos)
    {
        GameObject go = null;
        UnityEngine.Object obj = this.LocalLoad(strPath);
        if (obj)
        {
            go = obj as GameObject;
        }
        else
        {
            Debug.LogError("Load resource Failed :" + strPath);
            return null;
        }

        return AddChild(tsParent, go, vPos);
    }

    /// <summary>
    /// 添加子物体
    /// </summary>
    /// <param name="tsParent">父节点</param>
    /// <param name="go">子物体</param>
    /// <param name="vPos">初始位置</param>
    /// <returns>子物体</returns>
    public GameObject AddChild(Transform tsParent, GameObject go, Vector3 vPos)
    {
        if (null != go && null != tsParent)
        {
            Transform ts = go.transform;
            ts.SetParent(tsParent);
            ts.localPosition = vPos;
            ts.localRotation = Quaternion.identity;
            ts.localScale = Vector3.one;
            go.layer = tsParent.gameObject.layer;
        }

        if (null != go.GetComponent<GWindow>())
        {
            return go;
        }

        go.AddComponent<GWindow>();

        return go;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tsParent">父节点</param>
    /// <param name="strPath">子物体路径</param>
    /// <param name="vPos">初始位置</param>
    /// <param name="action">回调函数</param>
    public void AddChild(Transform tsParent, string strPath, Vector3 vPos, Action<GameObject> action)
    {
        ResLoad(strPath, typeof(GameObject), (obj) =>
            {
                GameObject go = AddChild(tsParent, obj as GameObject, vPos);
                if (null != go && null != action)
                {
                    action(go);
                }
            });
    }

    public void UnLoadUnuserdAssets()
    {
        Resources.UnloadUnusedAssets();
    }

    public void UnLoadAsset(UnityEngine.Object assetToUnload)
    {
        Resources.UnloadAsset(assetToUnload);
    }
}
