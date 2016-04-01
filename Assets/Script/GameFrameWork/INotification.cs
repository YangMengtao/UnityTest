using System;

namespace Game.FrameWork.InterFaces
{
    public interface INotification
    {
        object Body
        {
            get;
            set;
        }

        string Type
        {
            get;
            set;
        }
    }
}
