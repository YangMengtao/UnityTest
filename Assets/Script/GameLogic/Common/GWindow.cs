using UnityEngine;
using System.Collections;

public class GWindow : MonoBehaviour
{
    public enum EGWinType
    {
        Normal,
        PopUP,
        MainUI,
    }

    public string m_strPreWin = "";
    public string m_strWinName = "";
    public bool m_bIsHidden = true;

    public EGWinType m_WinType = EGWinType.Normal;

    public virtual void OnShow(object data = null) { }

    public virtual void OnHide() { }
}
