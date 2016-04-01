using System;
using System.Collections.Generic;

namespace Game.FrameWork.InterFaces
{
    public interface INoticeResponder
    {
        IList<string> NotificationList();

        void NotifyHandler(INotification notification);
    }
}
