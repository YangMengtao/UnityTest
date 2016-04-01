using System;

namespace Game.FrameWork.InterFaces
{
    public interface IFacade : INoticeDispatcher, ISender
    {
        void NotifyObserver(INotification notification);
    }
}
