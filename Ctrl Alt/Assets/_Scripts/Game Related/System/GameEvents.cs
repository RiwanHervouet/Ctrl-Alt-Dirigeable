using System;
using System.Collections;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    #region Events
    public event Action onNextTurn;

    #endregion


    public void NextTurn()
    {
        if (onNextTurn != null)
        {
            onNextTurn();
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
