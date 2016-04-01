using UnityEngine;
using System.Collections;

/// <summary>
/// 所有Log输出从这里调用，如果还有其他请自行添加
/// </summary>
public class LogPrint : MonoBehaviour
{
    private static LogPrint _instance;
    private bool m_isCanShowLog;
    public bool IsCanShowLog
    {
        get { return m_isCanShowLog; }
        set { m_isCanShowLog = value; }
    }

    public static LogPrint Instance
    {
        get
        {
            if (null == _instance)
            {
                GameObject go = new GameObject("_LogPrint");
                _instance = go.AddComponent<LogPrint>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 普通Log
    /// </summary>
    /// <param name="strContent"></param>
    public void OnPrint(string strContent)
    {
        if (!m_isCanShowLog)
            return;

        Debug.Log(strContent);
    }

    /// <summary>
    /// 错误Log
    /// </summary>
    /// <param name="strContent"></param>
    public void OnPrintError(string strContent)
    {
        if (!m_isCanShowLog)
            return;

        Debug.Log(strContent);
    }
}
