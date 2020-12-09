using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class GameSounds : MonoBehaviour
{
    #region Initialization
    public AudioSource fxSource; // joue un son UNE FOIS
    public AudioSource secondSource; // joue un son UNE FOIS
    public AudioSource ambianceSource; // loop le seul son dedans

    //Hits
    public AudioClip lightningHitClip;
    public AudioClip mountainHitClip;
    public AudioClip electricalShortCircuitClip;
    public AudioClip gameOverClip;

    //Modules
    public AudioClip accelerationCLip;
    public AudioClip deccelerationClip;
    public AudioClip altitudeDecliningClip;
    public AudioClip altitudeIncreasingClip;
    public AudioClip navigationClip;
    public AudioClip repairClip;

    //Second
    //Second Intro
    public AudioClip secondWelcomeClip;
    public AudioClip secondAltitudeClip;
    public AudioClip secondNavigationClip;
    public AudioClip secondAccelerationClip;
    public AudioClip secondRepairClip;
    public AudioClip secondFuelClip;
    public AudioClip secondGoalClip;
    public AudioClip secondConclusionClip;
    //Second Weather
    public AudioClip secondHeavyRainClip;
    public AudioClip secondMountainStraightAheadClip;
    public AudioClip secondLightningRiskClip;
    //Second Hits
    public AudioClip secondElectricalShortCircuitClip;
    public AudioClip secondMountainHitClip;
    public AudioClip secondLightningHitClip;
    public AudioClip secondGameOverClip;
    //Second Others
    public AudioClip secondApproachMapEdgesClip;
    public AudioClip secondMapEdgesCrossedClip;
    public AudioClip secondLowFuelClip;
    public AudioClip secondOverheatingRiskClip;
    public AudioClip secondOverheatingClip;
    //Second Reminds
    public AudioClip secondChangeDirectionClip;
    public AudioClip secondVaryAltitudeClip;
    public AudioClip secondAccelerateClip;
    public AudioClip secondRemindRepairClip;
    public AudioClip secondRemindOverheatingClip;
    public AudioClip secondRemindGoalClip;
    //Second Congratulations
    public AudioClip secondCongratRepairClip;
    public AudioClip secondCongratPowerResetClip;
    public AudioClip secondCongratDangerAvoidClip;
    public AudioClip secondCongratSpeedClip;
    public AudioClip secondCongratGoalAchievedClip;

    //Alarms
    public AudioClip alarmMapEdgesClip;
    public AudioClip alarmHitClip;
    public AudioClip alarmCriticalDamageClip;

    //Clickers
    public AudioClip clickerNavigationClip;
    public AudioClip clickerRepairClip;
    public AudioClip clickerAltitudeClip;
    public AudioClip clickerAccelerationClip;
    public AudioClip clickerDeccelerationClip;

    #endregion

    void Start()
    {
        //Hits
        GameEvents.Instance.OnMountainHit += PlayMountainHitSound;
        GameEvents.Instance.OnLightningHit += PlayLightningHitSound;
        GameEvents.Instance.OnElectricalShortCircuit += PlayElectricalShortCircuitSound;
        GameEvents.Instance.OnGameOver += PlayGameOverSound;
        //GameEvents.Instance.OnShipHit1 += PlayShipHit1Sound;

        //Modules
        GameEvents.Instance.OnAcceleration += PlaySecondAccelerationSound;
        GameEvents.Instance.OnDecceleration += PlayDeccelerationSound;
        GameEvents.Instance.OnAltitudeDeclining += PlayAltitudeDecliningSound;
        GameEvents.Instance.OnAltitudeIncreasing += PlayAltitudeIncreasingSound;
        GameEvents.Instance.OnRepair += PlayRepairSound;
        GameEvents.Instance.OnNavigation += PlayNavigationSound;

        //Second
        //Second Intro
        GameEvents.Instance.OnSecondWelcome += PlaySecondWelcomeSound;
        GameEvents.Instance.OnSecondAltitude += PlaySecondAltitudeSound;
        GameEvents.Instance.OnSecondNavigation += PlaySecondNavigationSound;
        GameEvents.Instance.OnSecondAcceleration += PlaySecondAccelerationSound;
        GameEvents.Instance.OnSecondRepair += PlaySecondRepairSound;
        GameEvents.Instance.OnSecondFuel += PlaySecondFuelSound;
        GameEvents.Instance.OnSecondGoal += PlaySecondGoalSound;
        GameEvents.Instance.OnSecondConclusion += PlaySecondConclusionSound;
        //Second Weather
        GameEvents.Instance.OnSecondHeavyRain += PlaySecondHeavyRainSound;
        GameEvents.Instance.OnSecondMountainStraightAhead += PlaySecondMountainStraightAheadSound;
        GameEvents.Instance.OnSecondLightningRisk += PlaySecondLightningRiskSound;
        //Second Hits
        GameEvents.Instance.OnSecondElectricalShortCircuit += PlaySecondElectricalShortCircuitSound;
        GameEvents.Instance.OnSecondMountainHit += PlaySecondMountainHitSound;
        GameEvents.Instance.OnSecondLightningHit += PlaySecondLightningHitSound;
        GameEvents.Instance.OnSecondGameOver += PlaySecondGameOverSound;
        //Second Others
        GameEvents.Instance.OnSecondApproachMapEdges += PlaySecondApproachMapEdgesSound;
        GameEvents.Instance.OnSecondMapEdgesCrossed += PlaySecondMapEdgesCrossedSound;
        GameEvents.Instance.OnSecondLowFuel += PlaySecondLowFuelSound;
        GameEvents.Instance.OnSecondOverheatingRisk += PlaySecondOverheatingRiskSound;
        GameEvents.Instance.OnSecondOverheating += PlaySecondOverheatingSound;
        //Second Reminds
        GameEvents.Instance.OnSecondChangeDirection += PlaySecondChangeDirectionSound;
        GameEvents.Instance.OnSecondVaryAltitude += PlaySecondVaryAltitudeSound;
        GameEvents.Instance.OnSecondAccelerate += PlaySecondAccelerateSound;
        GameEvents.Instance.OnSecondRemindRepair += PlaySecondRemindRepairSound;
        GameEvents.Instance.OnSecondRemindOverheating += PlaySecondRemindOverheatingSound;
        GameEvents.Instance.OnSecondRemindGoal += PlaySecondRemindGoalSound;
        //Second Congratulations
        GameEvents.Instance.OnSecondCongratRepair += PlaySecondCongratRepairSound;
        GameEvents.Instance.OnSecondCongratPowerReset += PlaySecondCongratPowerResetSound;
        GameEvents.Instance.OnSecondCongratDangerAvoid += PlaySecondCongratDangerAvoidSound;
        GameEvents.Instance.OnSecondCongratDangerAvoid += PlaySecondCongratDangerAvoidSound;
        GameEvents.Instance.OnSecondCongratDangerAvoid += PlaySecondCongratDangerAvoidSound;

        //Alarms
        GameEvents.Instance.OnAlarmMapEdges += PlayAlarmMapEdgesSound;
        GameEvents.Instance.OnShipIsSecure += StopLoopingSound;

        GameEvents.Instance.OnAlarmHit += PlayAlarmHitSound;
        GameEvents.Instance.OnShipIsRepaired += StopLoopingSound;

        GameEvents.Instance.OnAlarmCriticalDamage += PlayAlarmCriticalDamageSound;
        GameEvents.Instance.OnShipIsOk += StopLoopingSound;

        //Clickers
        GameEvents.Instance.OnClickerNavigation += PlayClickerNavigationSound;
        GameEvents.Instance.OnClickerRepair += PlayClickerRepairSound;
        GameEvents.Instance.OnClickerAltitude += PlayClickerAltitudeSound;
        GameEvents.Instance.OnClickerAcceleration += PlayClickerAccelerationSound;
        GameEvents.Instance.OnClickerDecceleration += PlayClickerDeccelerationSound;
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            //Hits
            GameEvents.Instance.OnMountainHit -= PlayMountainHitSound;
            GameEvents.Instance.OnLightningHit -= PlayMountainHitSound;
            GameEvents.Instance.OnElectricalShortCircuit -= PlayElectricalShortCircuitSound;
            GameEvents.Instance.OnGameOver -= PlayGameOverSound;
            //GameEvents.Instance.OnShipHit1 -= PlayShipHit1Sound;

            //Modules
            GameEvents.Instance.OnAcceleration -= PlayAccelerationSound;
            GameEvents.Instance.OnDecceleration -= PlayDeccelerationSound;
            GameEvents.Instance.OnAltitudeDeclining -= PlayAltitudeDecliningSound;
            GameEvents.Instance.OnAltitudeIncreasing -= PlayAltitudeIncreasingSound;
            GameEvents.Instance.OnRepair -= PlayRepairSound;
            GameEvents.Instance.OnNavigation -= PlayNavigationSound;

            //Second
            //Second Intro
            GameEvents.Instance.OnSecondWelcome -= PlaySecondWelcomeSound;
            GameEvents.Instance.OnSecondAltitude -= PlaySecondAltitudeSound;
            GameEvents.Instance.OnSecondNavigation -= PlaySecondNavigationSound;
            GameEvents.Instance.OnSecondAcceleration -= PlaySecondAccelerationSound;
            GameEvents.Instance.OnSecondRepair -= PlaySecondRepairSound;
            GameEvents.Instance.OnSecondFuel -= PlaySecondFuelSound;
            GameEvents.Instance.OnSecondGoal -= PlaySecondGoalSound;
            GameEvents.Instance.OnSecondConclusion -= PlaySecondConclusionSound;
            //Second Weather
            GameEvents.Instance.OnSecondHeavyRain -= PlaySecondHeavyRainSound;
            GameEvents.Instance.OnSecondMountainStraightAhead -= PlaySecondMountainStraightAheadSound;
            GameEvents.Instance.OnSecondLightningRisk -= PlaySecondLightningRiskSound;
            //Second Hits
            GameEvents.Instance.OnSecondElectricalShortCircuit -= PlaySecondElectricalShortCircuitSound;
            GameEvents.Instance.OnSecondMountainHit -= PlaySecondMountainHitSound;
            GameEvents.Instance.OnSecondLightningHit -= PlaySecondLightningHitSound;
            GameEvents.Instance.OnSecondGameOver -= PlaySecondGameOverSound;
            //Second Others
            GameEvents.Instance.OnSecondApproachMapEdges -= PlaySecondApproachMapEdgesSound;
            GameEvents.Instance.OnSecondMapEdgesCrossed -= PlaySecondMapEdgesCrossedSound;
            GameEvents.Instance.OnSecondLowFuel -= PlaySecondLowFuelSound;
            GameEvents.Instance.OnSecondOverheatingRisk -= PlaySecondOverheatingRiskSound;
            GameEvents.Instance.OnSecondOverheating -= PlaySecondOverheatingSound;
            //Second Reminds
            GameEvents.Instance.OnSecondChangeDirection -= PlaySecondChangeDirectionSound;
            GameEvents.Instance.OnSecondVaryAltitude -= PlaySecondVaryAltitudeSound;
            GameEvents.Instance.OnSecondAccelerate -= PlaySecondAccelerateSound;
            GameEvents.Instance.OnSecondRemindRepair -= PlaySecondRemindRepairSound;
            GameEvents.Instance.OnSecondRemindOverheating -= PlaySecondRemindOverheatingSound;
            GameEvents.Instance.OnSecondRemindGoal -= PlaySecondRemindGoalSound;
            //Second Congratulations
            GameEvents.Instance.OnSecondCongratRepair -= PlaySecondCongratRepairSound;
            GameEvents.Instance.OnSecondCongratPowerReset -= PlaySecondCongratPowerResetSound;
            GameEvents.Instance.OnSecondCongratDangerAvoid -= PlaySecondCongratDangerAvoidSound;
            GameEvents.Instance.OnSecondCongratDangerAvoid -= PlaySecondCongratDangerAvoidSound;
            GameEvents.Instance.OnSecondCongratDangerAvoid -= PlaySecondCongratDangerAvoidSound;

            //Alarms
            GameEvents.Instance.OnAlarmMapEdges -= PlayAlarmMapEdgesSound;
            GameEvents.Instance.OnShipIsSecure -= StopLoopingSound;

            GameEvents.Instance.OnAlarmHit -= PlayAlarmHitSound;
            GameEvents.Instance.OnShipIsRepaired -= StopLoopingSound;

            GameEvents.Instance.OnAlarmCriticalDamage -= PlayAlarmCriticalDamageSound;
            GameEvents.Instance.OnShipIsOk -= StopLoopingSound;

            //Clickers
            GameEvents.Instance.OnClickerNavigation -= PlayClickerNavigationSound;
            GameEvents.Instance.OnClickerRepair -= PlayClickerRepairSound;
            GameEvents.Instance.OnClickerAltitude -= PlayClickerAltitudeSound;
            GameEvents.Instance.OnClickerAcceleration -= PlayClickerAccelerationSound;
            GameEvents.Instance.OnClickerDecceleration -= PlayClickerDeccelerationSound;
        }
    }
     
    //Hits
    private void PlayMountainHitSound()
    {
        fxSource.PlayOneShot(mountainHitClip);
    }
    private void PlayLightningHitSound()
    {
        fxSource.PlayOneShot(lightningHitClip);
    }
    private void PlayElectricalShortCircuitSound()
    {
        fxSource.PlayOneShot(electricalShortCircuitClip);
    }
    private void PlayGameOverSound()
    {
        fxSource.PlayOneShot(gameOverClip);
    }
    /*private void PlaySHipHit1Sound()
    {
        fxSource.PlayOneShot(shipHit1);
    } */

    //Modules
    private void PlayAccelerationSound()
    {
        fxSource.PlayOneShot(accelerationCLip);
    }
    private void PlayDeccelerationSound()
    {
        fxSource.PlayOneShot(deccelerationClip);
    }
    private void PlayAltitudeDecliningSound()
    {
        fxSource.PlayOneShot(altitudeDecliningClip);
    }
    private void PlayAltitudeIncreasingSound()
    {
        fxSource.PlayOneShot(altitudeIncreasingClip);
    }
    private void PlayRepairSound()
    {
        fxSource.PlayOneShot(repairClip);
    }
    private void PlayNavigationSound()
    {
        fxSource.PlayOneShot(navigationClip);
    }

    //Second
    //Second Intro
    private void PlaySecondWelcomeSound()
    {
        secondSource.PlayOneShot(secondWelcomeClip);
    }
    private void PlaySecondAltitudeSound()
    {
        secondSource.PlayOneShot(secondAltitudeClip);
    }
    private void PlaySecondNavigationSound()
    {
        secondSource.PlayOneShot(secondNavigationClip);
    }
    private void PlaySecondAccelerationSound()
    {
        secondSource.PlayOneShot(secondAccelerationClip);
    }
    private void PlaySecondRepairSound()
    {
        secondSource.PlayOneShot(secondRepairClip);
    }
    private void PlaySecondFuelSound()
    {
        secondSource.PlayOneShot(secondFuelClip);
    }
    private void PlaySecondGoalSound()
    {
        secondSource.PlayOneShot(secondGoalClip);
    }
    private void PlaySecondConclusionSound()
    {
        secondSource.PlayOneShot(secondConclusionClip);
    }
    //Second Weather
    private void PlaySecondHeavyRainSound()
    {
        secondSource.PlayOneShot(secondHeavyRainClip);
    }
    private void PlaySecondMountainStraightAheadSound()
    {
        secondSource.PlayOneShot(secondMountainStraightAheadClip);
    }
    private void PlaySecondLightningRiskSound()
    {
        secondSource.PlayOneShot(secondLightningRiskClip);
    }
    //Second Hits
    private void PlaySecondElectricalShortCircuitSound()
    {
        secondSource.PlayOneShot(secondElectricalShortCircuitClip);
    }
    private void PlaySecondMountainHitSound()
    {
        secondSource.PlayOneShot(secondMountainHitClip);
    }
    private void PlaySecondLightningHitSound()
    {
        secondSource.PlayOneShot(secondLightningHitClip);
    }
    private void PlaySecondGameOverSound()
    {
        secondSource.PlayOneShot(secondGameOverClip);
    }
    //Second Others
    private void PlaySecondApproachMapEdgesSound()
    {
        secondSource.PlayOneShot(secondApproachMapEdgesClip);
    }
    private void PlaySecondMapEdgesCrossedSound()
    {
        secondSource.PlayOneShot(secondMapEdgesCrossedClip);
    }
    private void PlaySecondLowFuelSound()
    {
        secondSource.PlayOneShot(secondLowFuelClip);
    }
    private void PlaySecondOverheatingRiskSound()
    {
        secondSource.PlayOneShot(secondOverheatingRiskClip);
    }
    private void PlaySecondOverheatingSound()
    {
        secondSource.PlayOneShot(secondOverheatingClip);
    }
    //Second Reminds
    private void PlaySecondChangeDirectionSound()
    {
        secondSource.PlayOneShot(secondChangeDirectionClip);
    }
    private void PlaySecondVaryAltitudeSound()
    {
        secondSource.PlayOneShot(secondVaryAltitudeClip);
    }
    private void PlaySecondAccelerateSound()
    {
        secondSource.PlayOneShot(secondAccelerateClip);
    }
    private void PlaySecondRemindRepairSound()
    {
        secondSource.PlayOneShot(secondRemindRepairClip);
    }
    private void PlaySecondRemindOverheatingSound()
    {
        secondSource.PlayOneShot(secondRemindOverheatingClip);
    }
    private void PlaySecondRemindGoalSound()
    {
        secondSource.PlayOneShot(secondRemindGoalClip);
    }
    //Second Congratulations
    private void PlaySecondCongratRepairSound()
    {
        secondSource.PlayOneShot(secondCongratRepairClip);
    }
    private void PlaySecondCongratPowerResetSound()
    {
        secondSource.PlayOneShot(secondCongratPowerResetClip);
    }
    private void PlaySecondCongratDangerAvoidSound()
    {
        secondSource.PlayOneShot(secondCongratDangerAvoidClip);
    }
    private void PlaySecondCongratSpeedSound()
    {
        secondSource.PlayOneShot(secondCongratSpeedClip);
    }
    private void PlaySecondCongratGoalAchievedSound()
    {
        secondSource.PlayOneShot(secondCongratGoalAchievedClip);
    }
    
    //Alarms
    private void PlayAlarmMapEdgesSound()
    {
        ambianceSource.clip(alarmMapEdgesClip);
        ambianceSource.Play();
    }
    private void PlayAlarmHitSound()
    {
        ambianceSource.clip(alarmHitClip);
        ambianceSource.Play();
    }
    private void PlayAlarmCriticalDamageSound()
    {
        ambianceSource.clip(alarmCriticalDamageClip);
        ambianceSource.Play();
    }

    private void StopLoopingSound()
    {
        ambianceSource.Stop();
    }

    //Clickers
    private void PlayClickerNavigationSound()
    {
        fxSource.PlayOneShot(clickerNavigationClip);
    }
    private void PlayClickerRepairSound()
    {
        fxSource.PlayOneShot(clickerRepairClip);
    }
    private void PlayClickerAltitudeSound()
    {
        fxSource.PlayOneShot(clickerAltitudeClip);
    }
    private void PlayClickerAccelerationSound()
    {
        fxSource.PlayOneShot(clickerAccelerationClip);
    }
    private void PlayClickerDeccelerationSound()
    {
        fxSource.PlayOneShot(clickerDeccelerationClip);
    }

}
