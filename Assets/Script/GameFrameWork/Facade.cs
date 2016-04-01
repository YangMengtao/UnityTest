using System;
using System.Collections.Generic;
using Game.FrameWork.InterFaces;

namespace Game.FrameWork.Core
{
    public class Facade : IFacade
    {
        private List<INotification> m_lNotifications;
        private List<INoticeResponder> m_lNoticeResponders;
        private Dictionary<string, IModel> m_dicModels;

        public Facade()
        {
            if (null == m_lNotifications)
            {
                m_lNotifications = new List<INotification>();
            }

            if (null == m_lNoticeResponders)
            {
                m_lNoticeResponders = new List<INoticeResponder>();
            }

            if (null == m_dicModels)
            {
                m_dicModels = new Dictionary<string, IModel>();
            }
        }

        public void NotifyObserver(INotification notification)
        {
            if (null == notification)
            {
                return;
            }

            if (!m_lNotifications.Contains(notification))
            {
                m_lNotifications.Add(notification);
            }

            foreach (var item in m_lNoticeResponders)
            {
                item.NotifyHandler(notification);
                break;
            }
        }

        public void RegisterModel (string modelName, IModel model)
        {
            if (!m_dicModels.ContainsKey(modelName))
            {
                m_dicModels[modelName] = model;
            }
        }

        public IModel GetModel(string modelName)
        {
            if (m_dicModels.ContainsKey(modelName))
            {
                return m_dicModels[modelName];
            }

            return null;
        }

        public void RemoveModel(string modelName)
        {
            if (m_dicModels.ContainsKey(modelName))
            {
                m_dicModels.Remove(modelName);
            }
        }

        public void RegisterResponder(INoticeResponder responder)
        {
            if (!m_lNoticeResponders.Contains(responder))
            {
                m_lNoticeResponders.Add(responder);
            }
        }

        public void RemoveResponder(INoticeResponder responder)
        {
            if (!m_lNoticeResponders.Contains(responder))
            {
                m_lNoticeResponders.Remove(responder);
            }
        }

        public void SendNotification(string type)
        {
            this.NotifyObserver(new Notification(type));
        }

        public void SendNotification(string type, object body)
        {
            this.NotifyObserver(new Notification(type, body));
        }
    }
}
