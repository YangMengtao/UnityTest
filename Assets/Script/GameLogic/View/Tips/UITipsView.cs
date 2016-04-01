using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Game.FrameWork.InterFaces;

public class UITipsView : GWindow, INoticeResponder
{
    private IList<string> _noticeList = new string[]
    {
        "AA",
    };

    public IList<string> NotificationList()
    {
        return _noticeList;
    }

    public void NotifyHandler(INotification note)
    {
        switch (note.Type)
        {
            case "AA":
                {
                    Debug.Log("AA");
                    break;
                }
//             case "BB":
//                 {
//                     string str = note.Body as string;
//                     Debug.Log(str);
//                     break;
//                 }
        }
    }

    public void OnClickOK(GameObject go)
    {
        Common.g_uiMgr.BackLastWin();
    }

    public override void OnShow(object data = null)
    {
        Debug.Log("Open win tips!");
    }

    public override void OnHide()
    {
        Debug.Log("Close win tips");
    }
}
