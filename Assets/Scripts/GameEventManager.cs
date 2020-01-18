using UnityEngine;
using System.Collections;
using System;

public class GameEventManager
{
    private static GameEventManager instance;

    public static GameEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEventManager();
            }
            return instance;
        }
    }

    // Private ctor ... Singleton!
    private GameEventManager()
    {
    }
    
    public Action OnBlackHoleSizeUp;
    public Action OnBlackHoleSizeDown;
    
}