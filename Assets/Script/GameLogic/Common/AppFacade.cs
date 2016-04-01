using UnityEngine;
using System.Collections;
using Game.FrameWork.Core;

public class AppFacade : Facade
{
    private static AppFacade _instance;

    public static AppFacade Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new AppFacade();
            }
            return _instance;
        }
    }

    public void RegisterModel()
    {
        
    }
}
