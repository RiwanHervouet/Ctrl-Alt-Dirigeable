using System;
using System.Collections;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    #region Events
    public event Action onNextTurn;
    public event Action onNextEnvironmentUpdate;
    public event Action onNextPlayerUpdate;

    #endregion

    #region Methods related to events
    public void NextTurn()
    {
        if (onNextTurn != null)
        {
            onNextTurn();
        }
    }
    public void NextEnvironmentUpdate()
    {
        if (onNextEnvironmentUpdate != null)
        {
            onNextEnvironmentUpdate();
        }
    }
    public void NextPlayerUpdate()
    {
        if (onNextPlayerUpdate != null)
        {
            onNextPlayerUpdate();
        }
    }
    #endregion
}
