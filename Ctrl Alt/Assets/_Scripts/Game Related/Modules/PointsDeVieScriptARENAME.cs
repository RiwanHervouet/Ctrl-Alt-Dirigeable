using System.Collections;
using UnityEngine;

public class PointsDeVieScriptARENAME : MonoBehaviour
{
    #region Initialization
    int pdv;
    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(pdv<=1)
        GameEvents.Instance.AlarmHitSound();
        GameEvents.Instance.SecondLowFuel();

        if(pdv<=0)
        {
            GameEvents.Instance.GameOver();
            GameEvents.Instance.SecondGameOver();
        }
    }
}
