using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.FrameWork.InterFaces;

public class UIManager : MonoBehaviour
{
    private const int MAX_OPEN_WIN_COUNT = 5;

    private Dictionary<string, GameObject> m_dicWin;
    private Dictionary<string, GameObject> m_dicHiddingWin;
    private Queue<string> m_qWinNanme;

    private GameObject m_goCurrentWin;

    void Awake()
    {
        m_dicWin = new Dictionary<string, GameObject>();
        m_dicHiddingWin = new Dictionary<string, GameObject>();
        m_qWinNanme = new Queue<string>();
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="winName">窗口名字</param>
    /// <param name="isRegistFacade">是否订阅</param>
    public void OpenWin(string winName, bool isRegistFacade)
    {
        GWindow gWin = null;

        if (m_dicWin.ContainsKey(winName))
        {
            m_goCurrentWin = (GameObject)m_dicWin[winName];
            gWin = m_goCurrentWin.GetComponent<GWindow>();
        }
        else
        {
            m_goCurrentWin = Common.g_assetMgr.AddChildByLocal(transform, winName);
            gWin = m_goCurrentWin.GetComponent<GWindow>();
            if (null == gWin)
            {
                gWin = m_goCurrentWin.AddComponent<GWindow>();
            }

            gWin.m_strWinName = winName;
            m_qWinNanme.Enqueue(winName);
            m_dicWin.Add(winName, m_goCurrentWin);
            if (m_qWinNanme.Count > MAX_OPEN_WIN_COUNT)
            {
                string strPopWinName = m_qWinNanme.Dequeue();
                if (m_dicHiddingWin.ContainsKey(strPopWinName))
                {
                    m_qWinNanme.Enqueue(strPopWinName);
                }
                else
                {
                    GameObject go = m_dicWin[strPopWinName];
                    m_dicWin.Remove(strPopWinName);
                    go.transform.parent = null;
                    UnityEngine.Object.Destroy(go);
                    Common.g_assetMgr.UnLoadUnuserdAssets();
                }
            }
        }

        if (gWin is INoticeResponder && isRegistFacade)
        {
            Common.g_appFacade.RegisterResponder(gWin as INoticeResponder);
        }
    }

    /// <summary>
    /// 关闭打开的窗口
    /// </summary>
    /// <param name="isDestory">是否销毁</param>
    public void CloseCurrentWin(bool isDestory)
    {
        if (null != m_goCurrentWin)
        {
            GWindow gWin = m_goCurrentWin.GetComponent<GWindow>();
            m_goCurrentWin.SetActive(false);
            if (isDestory)
            {
                gWin.OnHide();
            }
            else
            {
                m_dicHiddingWin.Add(gWin.m_strWinName, m_goCurrentWin);
            }

            if (gWin is INoticeResponder && isDestory)
            {
                //取消订阅
                Common.g_appFacade.RemoveResponder(gWin as INoticeResponder);
            }

            m_goCurrentWin = null;
        }
    }

    /// <summary>
    /// 切换窗口
    /// </summary>
    /// <param name="winName">窗口</param>
    /// <param name="data">数据</param>
    /// <param name="isDestory">是否销毁</param>
    /// <param name="isReloadCurrentWin">是否重新加载</param>
    public void SwithWin(string winName, object data = null, bool isDestory = false, bool isReloadCurrentWin = false)
    {
        if (isReloadCurrentWin && null != m_goCurrentWin)
        {
            m_goCurrentWin.GetComponent<GWindow>().OnShow(data);
            return;
        }

        if (m_dicWin.ContainsKey(winName) && m_dicWin[winName] == m_goCurrentWin)
        {
            return;
        }

        if (m_dicHiddingWin.ContainsKey(winName))
        {
            m_dicHiddingWin.Remove(winName);
        }

        string strPreWinName = "";
        if (null != m_goCurrentWin)
        {
            strPreWinName = m_goCurrentWin.GetComponent<GWindow>().m_strWinName;
        }

        this.CloseCurrentWin(isDestory);

        OpenWin(winName, true);

        m_goCurrentWin.SetActive(true);

        m_goCurrentWin.GetComponent<GWindow>().OnShow(data);

        //记录上一个打开的窗口
        m_goCurrentWin.GetComponent<GWindow>().m_strPreWin = strPreWinName;
    }

    public void BackLastWin(string winName = null, object data = null)
    {
         GWindow gWin = null;
        if (string.IsNullOrEmpty(winName))
        {
           gWin = m_goCurrentWin.GetComponent<GWindow>();
        }

        if (null == gWin)
        {
            Debug.LogError("未找到打开的窗口");
            return;
        }

        string strPreWinName = gWin.m_strPreWin;

        if (string.IsNullOrEmpty(strPreWinName))
        {
            Debug.LogError("没有前置窗口");
            return;
        }

        SwithWin(strPreWinName, data, true);
    }
}
