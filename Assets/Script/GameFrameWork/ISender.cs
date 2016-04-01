using System;

namespace Game.FrameWork.InterFaces
{
    public interface ISender
    {
        void SendNotification(string type);

        void SendNotification(string type, object body);
    }
}
