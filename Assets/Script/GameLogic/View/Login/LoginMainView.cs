using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Game.FrameWork.InterFaces;

public class LoginMainView : GWindow, INoticeResponder
{
    [SerializeField]
    private InputField m_uiInputUserName;

    [SerializeField]
    private InputField m_uiInputUserPassWord;

    [SerializeField]
    private Button m_uiBtnLogin;

    void Awake()
    {
        LogPrint.Instance.OnPrint("");
    }

    public void OnClickLogin(GameObject go)
    {
        Common.g_uiMgr.SwithWin(UIWinName.TIPS_WIN, "aaa");
        Common.g_appFacade.SendNotification("BB", "123445");
        Common.g_appFacade.SendNotification("AA");
    }

    private IList<string> _noticeList = new string[]
    {
        "BB",
    };

    public IList<string> NotificationList()
    {
        return _noticeList;
    }

    public void NotifyHandler(INotification note)
    {
        switch (note.Type)
        {
            case "BB":
                {
                    LogPrint.Instance.OnPrint(note.Body.ToString());
                    break;
                }
        }
    }

    public override void OnShow(object data = null)
    {
         LogPrint.Instance.OnPrint("Open win login ~~~~~!");
    }

    public override void OnHide()
    { 
         LogPrint.Instance.OnPrintError("这是一个测试!");
    }
}
