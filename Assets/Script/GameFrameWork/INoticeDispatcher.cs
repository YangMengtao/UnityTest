using System;

namespace Game.FrameWork.InterFaces
{
    public interface INoticeDispatcher : ISender
    {
        void RegisterResponder(INoticeResponder responder);

        void RemoveResponder(INoticeResponder responder);
    }
}
