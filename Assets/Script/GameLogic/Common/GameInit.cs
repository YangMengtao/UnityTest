using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour
{
    public Transform m_tsRoot;

    void Start()
    {
        Common.g_appFacade = AppFacade.Instance;
        Common.g_assetMgr = null == gameObject.GetComponent<AssetManager>() ? gameObject.AddComponent<AssetManager>() : gameObject.GetComponent<AssetManager>();
        Common.g_uiMgr = null == gameObject.GetComponent<UIManager>() ? gameObject.AddComponent<UIManager>() : gameObject.GetComponent<UIManager>();

        LogPrint.Instance.IsCanShowLog = true;
        
        Common.g_uiMgr.OpenWin(UIWinName.LOGIN_WIN, true);
    }
}
