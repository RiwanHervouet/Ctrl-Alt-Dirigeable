using System;
using System.Collections;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    #region Updates related
    #region Events
    public event Action onNextPlayerUpdate;

    public event Action onNextEnvironmentUpdate;
    //public event Action onNextEnvironmentUpdateMountain;
    //public event Action onNextEnvironmentUpdateStorm;

    public event Action onNextRefresh;
    #endregion

    #region Methods related to events
    public void NextRefresh()
    {
        if (onNextRefresh != null)
        {
            onNextRefresh();
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
    #endregion



    #region Input related
    #region Events
    /*public event Action onPlayerConfirmedGaze;
    #endregion

    #region Methods related to events
    public void PlayerConfirmedGaze()
    {
        if (onPlayerConfirmedGaze != null)
        {
            onPlayerConfirmedGaze();
        }
    }*/
    #endregion
    #endregion
}
