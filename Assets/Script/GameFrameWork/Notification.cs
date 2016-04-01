using System;
using Game.FrameWork.InterFaces;

namespace Game.FrameWork.Core
{
    public class Notification : INotification
    {
        private object body;

        public virtual object Body
        {
            get { return body; }
            set { body = value; }
        }

        private string type;

        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Notification(string type, object body = null)
        {
            this.Type = type;
            this.Body = body;
        }
    }
}
