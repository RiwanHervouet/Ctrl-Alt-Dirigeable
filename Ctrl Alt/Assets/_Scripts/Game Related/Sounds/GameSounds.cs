using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class GameSounds : MonoBehaviour
{
    #region Initialization
    public AudioSource fxSource;
    public AudioSource ambianceSource;
    public AudioClip mountainHitClip;
    #endregion

    void Start()
    {
        GameEvents.Instance.OnOnMountainHitHit += PlayMountainHitSound;
        //GameEvents.Instance.OnAlarmEnabled += PlayAlarmSound;
        //GameEvents.Instance.OnShipProblemResolved += StopLoopingSound;
    }

    private void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnOnMountainHitHit -= PlayMountainHitSound;
            //GameEvents.Instance.OnAlarmEnabled -= PlayAlarmSound;
            //GameEvents.Instance.OnShipProblemResolved -= StopLoopingSound;
        }
    }

    private void PlayMountainHitSound()
    {
        fxSource.PlayOneShot(mountainHitClip);
    }
    /*
    private void PlayAlarmSound()
    {
        ambianceSource.clip(alarmClip);
        ambianceSource.Play();
    }

    private void StopLoopingSound()
    {
        ambianceSource.Stop();
    }*/
}
