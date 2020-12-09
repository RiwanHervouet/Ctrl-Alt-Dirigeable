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
    public AudioClip secondMoutainStraightAhead;
    public AudioClip secondLightningRisk;

    //Second Hits
    //Second Others
    //Second Reminds
    //Second Gratifications

    //public AudioClip shipHit1Clip;

    #endregion

    void Start()
    {
        //Hits
        GameEvents.Instance.OnMountainHit += PlayMountainHitSound;
        GameEvents.Instance.OnLightningHit += PlayLightningHitSound;
        GameEvents.Instance.OnElectricalShortCircuit += PlayElectricalShortCircuitSound;
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
        GameEvents.Instance.OnSecondMoutainStraightAhead += PlaySecondMountainStraightAheadSound;
        GameEvents.Instance.OnSecondLightningRisk += PlaySecondLightningRiskSound;
        


        //Alarms
        GameEvents.Instance.OnAlarmEnabled += PlayAlarmSound;
        GameEvents.Instance.OnShipProblemResolved += StopLoopingSound;
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            //Hits
            GameEvents.Instance.OnMountainHit -= PlayMountainHitSound;
            GameEvents.Instance.OnLightningHit -= PlayMountainHitSound;
            GameEvents.Instance.OnElectricalShortCircuit -= PlayElectricalShortCircuitSound;
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

        

            GameEvents.Instance.OnAlarmEnabled -= PlayAlarmSound;
            GameEvents.Instance.OnShipProblemResolved -= StopLoopingSound;
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
       secondSource.PlayOneShot(secondAltitudeClip)
    }
    private void PlaySecondNavigationSound()
    {
        secondSource.PlayOneShot(secondNavigationClip)
    }
    private void PlaySecondAccelerationSound()
    {
        secondSource.PlayOneShot(secondAccelerationClip)
    }
    private void PlaySecondRepairSound()
    {
        secondSource.PlayOneShot(secondRepairClip)
    }
    private void PlaySecondFuelSound()
    {
        secondSource.PlayOneShot(secondFuelClip)
    }
    private void PlaySecondGoalSound()
    {
        secondSource.PlayOneShot(secondGoalClip)
    }
    private void PlaySecondConclusionSound()
    {
        secondSource.PlayOneShot(secondConclusionClip)
    }

    //Alarms
private void PlayAlarmSound()
    {
        ambianceSource.clip(alarmClip);
        ambianceSource.Play();
    }

    private void StopLoopingSound()
    {
        ambianceSource.Stop();
    }
}
