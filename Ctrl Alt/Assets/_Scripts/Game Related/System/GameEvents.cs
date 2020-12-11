using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    #region Updates related
    #region Events
    public event Action OnJustBeforeNextPlayerUpdate;
    public event Action OnNextPlayerUpdate;

    public event Action OnNextEnvironmentUpdate;
    //public event Action onNextEnvironmentUpdateStart;
    //public event Action onNextEnvironmentUpdateData;

    public event Action OnNextRefresh;

    public event Action OutOfTimeEvents;
    #endregion

    #region Methods related to events
    public void NextRefresh()
    {
        OnNextRefresh?.Invoke();
    }
    public void NextEnvironmentUpdate()
    {
        OnNextEnvironmentUpdate?.Invoke();
    }
    public void NextPlayerUpdate()
    {
        OnNextPlayerUpdate?.Invoke();
    }
    public void JustBeforeNextPlayerUpdate()
    {
        OnJustBeforeNextPlayerUpdate?.Invoke();
    }
    public void OutOfTimeUpdate()
    {
        OutOfTimeEvents?.Invoke();
    }
    #endregion
    #endregion



    #region Input related
    #region Events
    public event Func<Vector2> OnPlayerDirectionChange;
    public event Action<List<physicalObjectType>> OnPlayerGettingHit;
    public event Action OnShipRepaired;
    public event Action<Inputs.inputs, bool> OnCtrlAltInputSent;
    #endregion

    #region Methods related to events
    public Vector2 PlayerDirectionChange()
    {
        if (OnPlayerDirectionChange != null)
        {
            if (OnPlayerDirectionChange() == Vector2.zero)
            {
                OnPlayerDirectionChange = null;
            }

            if (OnPlayerDirectionChange != null)
            {
                return OnPlayerDirectionChange();
            }
        }
        return Vector2.zero;
    }

    public void PlayerIsHit(List<physicalObjectType> objectHit)
    {
        if (OnPlayerGettingHit != null)
        {
            OnPlayerGettingHit(objectHit);
        }
    }

    public void RepairShip() //not called yet
    {
        OnShipRepaired?.Invoke();
    }

    public void CtrlAltInputSent(Inputs.inputs inputSent, bool didISendIt)
    {
        if (OnCtrlAltInputSent != null)
            OnCtrlAltInputSent(inputSent, didISendIt);
    }
    #endregion
    #endregion



    #region Output related
    #region Events
    public event Action<float, Inputs.inputs> OnMapInputCompletion;
    public event Action OnMapInputCompleted;
    #endregion

    #region Methods related to events
    public void MapInputCompletion(float inputCompletionPercentage, Inputs.inputs input)
    {
        if (OnMapInputCompletion != null)
        {
            OnMapInputCompletion(inputCompletionPercentage, input);
        }
    }

    public void MapInputCompleted()
    {
        OnMapInputCompleted?.Invoke();
    }
    #endregion
    #endregion


    #region Feedback related
    #region Events
    public event Action OnOnMountainHitHit;
    #endregion

    #region Methods related to events
    public void MountainHit()
    {
        OnOnMountainHitHit?.Invoke();
    }
    #endregion
    #endregion
}
