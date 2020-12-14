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

    public event Action OnJustBeforeNextEnvironmentUpdate;
    public event Action OnNextEnvironmentUpdate;

    public event Action OnJustBeforeNextRefresh;
    public event Action OnNextRefresh;

    public event Action OutOfTimeEvents;
    #endregion

    #region Methods related to events
    public void NextRefresh()
    {
        OnNextRefresh?.Invoke();
    }
    public void JustBeforeNextRefresh()
    {
        OnJustBeforeNextRefresh?.Invoke();
    }
    public void NextEnvironmentUpdate()
    {
        OnNextEnvironmentUpdate?.Invoke();
    }
    public void JustBeforeNextEnvironmentUpdate()
    {
        OnJustBeforeNextEnvironmentUpdate?.Invoke();
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
    public event Action<bool> OnSouffleurInhaled;
    public event Action<bool> OnSouffleurExhaled;
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

    public void SouffleurInhaled(bool isInhaled)
    {
        if (OnSouffleurInhaled != null)
            OnSouffleurInhaled(isInhaled);
    }

    public void SouffleurExhaled(bool isExhaled)
    {
        if (OnSouffleurExhaled != null)
            OnSouffleurExhaled(isExhaled);
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
    //LEDsOn
    public event Action OnTurnOnEssence1;
    public event Action OnTurnOnEssence2;
    public event Action OnTurnOnEssence3;
    public event Action OnTurnOnPause;
    public event Action OnTurnOnRappel;
    public event Action OnTurnOnAltitude1;
    public event Action OnTurnOnAltitude2;
    public event Action OnTurnOnAltitude3;
    public event Action OnTurnOnConfirmationAltitude;
    public event Action OnTurnOnConfirmationEssence;
    public event Action OnTurnOnConfirmationVitesse;
    //LEDsOff
    public event Action OnTurnOffEssence1;
    public event Action OnTurnOffEssence2;
    public event Action OnTurnOffEssence3;
    public event Action OnTurnOffPause;
    public event Action OnTurnOffRappel;
    public event Action OnTurnOffAltitude1;
    public event Action OnTurnOffAltitude2;
    public event Action OnTurnOffAltitude3;
    public event Action OnTurnOffConfirmationAltitude;
    public event Action OnTurnOffConfirmationEssence;
    public event Action OnTurnOffConfirmationVitesse;

    //Hits
    public event Action OnMountainHit;
    public event Action OnLightningHit;
    public event Action OnElectricalShortCircuit;
    public event Action OnGameOver;

    //Modules
    public event Action OnAcceleration;
    public event Action OnDecceleration;
    public event Action OnAltitudeDeclining;
    public event Action OnAltitudeIncreasing;
    public event Action OnRepair;
    public event Action OnNavigation;

    //Second
    //Second Intro
    public event Action OnSecondWelcome;
    public event Action OnSecondNavigation;
    public event Action OnSecondAltitude;
    public event Action OnSecondAcceleration;
    public event Action OnSecondRepair;
    public event Action OnSecondFuel;
    public event Action OnSecondGoal;
    public event Action OnSecondConclusion;
    //Second Weather
    public event Action OnSecondHeavyRain;
    public event Action OnSecondMountainStraightAhead;
    public event Action OnWithinAStorm;
    public event Action OnExitStorm;
    //Second Hits
    public event Action OnSecondElectricalShortCircuit;
    public event Action OnSecondMountainHit;
    public event Action OnSecondLightningHit;
    public event Action OnSecondGameOver;
    //Second Others
    public event Action OnSecondApproachMapEdges;
    public event Action OnSecondMapEdgesCrossed;
    public event Action OnSecondLowFuel;
    public event Action OnSecondOverheatingRisk;
    public event Action OnSecondOverheating;
    //Second Reminds
    public event Action OnSecondChangeDirection;
    public event Action OnSecondVaryAltitude;
    public event Action OnSecondAccelerate;
    public event Action OnSecondRemindRepair;
    public event Action OnSecondRemindOverheating;
    public event Action OnSecondRemindGoal;
    //Second Gratifications
    public event Action OnSecondCongratRepair;
    public event Action OnSecondCongratPowerReset;
    public event Action OnSecondCongratDangerAvoid;
    public event Action OnSecondCongratSpeed;
    public event Action OnSecondCongratGoalAchieved;

    //Alarms
    public event Action OnAlarmMapEdges;
    public event Action OnAlarmHit;

    //Clickers
    public event Action OnClickerNavigation;
    public event Action OnClickerRepair;
    public event Action OnClickerAltitude;
    public event Action OnClickerAcceleration;
    public event Action OnClickerDecceleration;
    #endregion

    #region Methods related to events
    //LEDsOn
    public void TurnOnEssence()
    {
        OnTurnOnEssence1?.Invoke();
    }
    public void TurnOnEssence2()
    {
        OnTurnOnEssence2?.Invoke();
    }
    public void TurnOnEssence3()
    {
        OnTurnOnEssence3?.Invoke();
    }
    public void TurnOnPause()
    {
        OnTurnOnPause?.Invoke();
    }
    public void TurnOnRappel()
    {
        OnTurnOnRappel?.Invoke();
    }
    public void TurnOnAltitude1()
    {
        OnTurnOnAltitude1?.Invoke();
    }
    public void TurnOnAltitude2()
    {
        OnTurnOnAltitude2?.Invoke();
    }
    public void TurnOnAltitude3()
    {
        OnTurnOnAltitude3?.Invoke();
    }
    public void TurnOnConfirmationAltitude()
    {
        OnTurnOnConfirmationAltitude?.Invoke();
    }
    public void TurnOnConfirmationEssence()
    {
        OnTurnOnConfirmationEssence?.Invoke();
    }
    public void TurnOnConfirmationVitesse()
    {
        OnTurnOnConfirmationEssence?.Invoke();
    }

    //LEDsOff           
    public void TurnOffEssence1()
    {
        OnTurnOffEssence1?.Invoke();
    }
    public void TurnOffEssence2()
    {
        OnTurnOffEssence2?.Invoke();
    }
    public void TurnOffEssence3()
    {
        OnTurnOffEssence3?.Invoke();
    }
    public void TurnOffPause()
    {
        OnTurnOffPause?.Invoke();
    }
    public void TurnOffRappel()
    {
        OnTurnOffRappel?.Invoke();
    }
    public void TurnOffAltitude1()
    {
        OnTurnOffAltitude1?.Invoke();
    }
    public void TurnOffAltitude2()
    {
        OnTurnOffAltitude2?.Invoke();
    }
    public void TurnOffAltitude3()
    {
        OnTurnOffAltitude3?.Invoke();
    }
    public void TurnOffConfirmationAltitude()
    {
        OnTurnOffConfirmationAltitude?.Invoke();
    }
    public void TurnOffConfirmationEssence()
    {
        OnTurnOffConfirmationEssence?.Invoke();
    }
    public void TurnOffConfirmationVitesse()
    {
        OnTurnOffConfirmationVitesse?.Invoke();
    }

    //Hits
    public void MountainHit()
    {
        OnMountainHit?.Invoke();
    }
    public void LightningHit()
    {
        OnLightningHit?.Invoke();
    }
    public void ElectricalShortCircuit()
    {
        OnElectricalShortCircuit?.Invoke();
    }
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    //Modules
    public void Acceleration()
    {
        OnAcceleration?.Invoke();
    }
    public void Decceleration()
    {
        OnDecceleration?.Invoke();
    }
    public void AltitudeDeclining()
    {
        OnAltitudeDeclining?.Invoke();
    }
    public void AltitudeIncreasing()
    {
        OnAltitudeIncreasing?.Invoke();
    }
    public void Repair()
    {
        OnRepair?.Invoke();
    }
    public void Navigation()
    {
        OnNavigation?.Invoke();
    }

    //Second
    //Second Intro
    public void SecondWelcome()
    {
        OnSecondWelcome?.Invoke();
    }
    public void SecondAltitude()
    {
        OnSecondAltitude?.Invoke();
    }
    public void NavigationWelcome()
    {
        OnSecondNavigation?.Invoke();
    }
    public void SecondAcceleration()
    {
        OnSecondAcceleration?.Invoke();
    }
    public void SecondRepair()
    {
        OnSecondRepair?.Invoke();
    }
    public void SecondFuel()
    {
        OnSecondFuel?.Invoke();
    }
    public void SecondGoal()
    {
        OnSecondGoal?.Invoke();
    }
    public void SecondConclusion()
    {
        OnSecondConclusion?.Invoke();
    }
    //Second Weather
    public void SecondHeavyRain()
    {
        OnSecondHeavyRain?.Invoke();
    }
    public void SecondMountainStraightAhead()
    {
        OnSecondMountainStraightAhead?.Invoke();
    }
    public void EnteringAStorm()
    {
        OnWithinAStorm?.Invoke();
    }
    public void ExitingAStorm()
    {
        OnExitStorm?.Invoke();
    }
    //Second Hits
    public void SecondElectricalShortCircuit()
    {
        OnSecondElectricalShortCircuit?.Invoke();
    }
    public void SecondMountainHit()
    {
        OnSecondMountainHit?.Invoke();
    }
    public void SecondLightningHit()
    {
        OnSecondLightningHit?.Invoke();
    }
    public void SecondGameOver()
    {
        OnSecondGameOver?.Invoke();
    }
    //Second Others
    public void SecondApproachMapEdges()
    {
        OnSecondApproachMapEdges?.Invoke();
    }
    public void SecondMapEdgesCrossed()
    {
        OnSecondMapEdgesCrossed?.Invoke();
    }
    public void SecondLowFuel()
    {
        OnSecondLowFuel?.Invoke();
    }
    public void SecondOverheatingRisk()
    {
        OnSecondOverheatingRisk?.Invoke();
    }
    public void SecondOverheating()
    {
        OnSecondOverheating?.Invoke();
    }
    //Second Reminds
    public void SecondChangeDirection()
    {
        OnSecondChangeDirection?.Invoke();
    }
    public void SecondVaryAltitude()
    {
        OnSecondVaryAltitude?.Invoke();
    }
    public void SecondAccelerate()
    {
        OnSecondAccelerate?.Invoke();
    }
    public void SecondRemindRepair()
    {
        OnSecondRemindRepair?.Invoke();
    }
    public void SecondRemindOverheating()
    {
        OnSecondRemindOverheating?.Invoke();
    }
    public void SecondRemindGoal()
    {
        OnSecondRemindGoal?.Invoke();
    }
    //Second Gratifications
    public void SecondCongratRepair()
    {
        OnSecondCongratRepair?.Invoke();
    }
    public void SecondCongratPowerReset()
    {
        OnSecondCongratPowerReset?.Invoke();
    }
    public void SecondCongratDangerAvoid()
    {
        OnSecondCongratDangerAvoid?.Invoke();
    }
    public void SecondCongratSpeed()
    {
        OnSecondCongratSpeed?.Invoke();
    }
    public void SecondCongratGoalAchieved()
    {
        OnSecondCongratGoalAchieved?.Invoke();
    }

    //Alarms

    public void AlarmMapEdges()
    {
        OnAlarmMapEdges?.Invoke();
    }
    public void AlarmHitSound()
    {
        OnAlarmHit?.Invoke();
    }

    //Clickers
    public void ClickerNavigation()
    {
        OnClickerNavigation?.Invoke();
    }
    public void ClickerRepair()
    {
        OnClickerRepair?.Invoke();
    }
    public void ClickerAltitude()
    {
        OnClickerAltitude?.Invoke();
    }
    public void ClickerAcceleration()
    {
        OnClickerAcceleration?.Invoke();
    }
    public void ClickerDecceleration()
    {
        OnClickerDecceleration?.Invoke();
    }
    #endregion
    #endregion
}
