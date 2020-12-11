﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Ce script s'occupe de toute la temporalité du jeu. 
/// 
/// </summary>
public class GameTime : Singleton<GameTime>
{
    #region Initialization

    #region Interesting informations
    [Header("Game time characteristics")]
    [Tooltip("Le temps de rafraichissement de la matrice.")]
    [SerializeField] [Range(0.1f, 2f)] private float refreshRate = .5f;

    [Tooltip("Tous les combien de frames les éléments autres que le joueurs sont update ?")]
    [SerializeField] [Range(1, 8)] private int environmentUpdateRate = 4;

    [Tooltip("Tous les combien de frames le joueurs est update ?")]
    [SerializeField] [Range(1, 8)] private int playerUpdateRate = 4;

    public bool gameIsPaused = false;
    #endregion

    #region Public informations
    public float RefreshRate { get => refreshRate; }
    #endregion

    #region Making code work related
    private float timeUntilUpdate = 0f;
    private int updatesUntilEnvironmentUpdate = 0;
    private int updatesUntilPlayerUpdate = 0;
    #endregion

    #endregion

    void Start()
    {
        StartLevel();
    }
    void StartLevel()
    {
        timeUntilUpdate = refreshRate;
        playerUpdateRate = environmentUpdateRate;
    }

    void Update()
    {
        if (!gameIsPaused)
        {
            timeUntilUpdate -= Time.deltaTime;
            if (timeUntilUpdate <= 0f)
            {
                if (updatesUntilEnvironmentUpdate <= 1)
                {
                    GameEvents.Instance.NextEnvironmentUpdate();
                    updatesUntilEnvironmentUpdate = environmentUpdateRate;
                }
                else
                {
                    updatesUntilEnvironmentUpdate--;
                }

                if (updatesUntilPlayerUpdate <= 1)
                {
                    GameEvents.Instance.JustBeforeNextPlayerUpdate();
                    GameEvents.Instance.NextPlayerUpdate();
                    updatesUntilPlayerUpdate = playerUpdateRate;
                }
                else
                {
                    updatesUntilPlayerUpdate--;
                }

                GameEvents.Instance.NextRefresh();

                timeUntilUpdate = refreshRate;
            }

            GameEvents.Instance.OutOfTimeUpdate();
        }
    }


    public void ChangePlayerSpeed(Inputs.inputs goFEST)
    {
        switch (goFEST)
        {
            case Inputs.inputs.SPEED_SLOWER:
                if (playerUpdateRate == 4)
                {
                    ChangePlayerUpdateRate(playerSpeed.SLOW);
                }
                else if (playerUpdateRate == 8)
                {
                    ChangePlayerUpdateRate(playerSpeed.MEDIUM);
                }
                break;
            case Inputs.inputs.SPEED_FASTER:
                if (playerUpdateRate == 4)
                {
                    ChangePlayerUpdateRate(playerSpeed.FAST);
                }
                else if (playerUpdateRate == 2)
                {
                    ChangePlayerUpdateRate(playerSpeed.MEDIUM);
                }
                break;
            default:
                Debug.LogWarning("Il faut rentrer une vitesse dans ce switch");
                break;
        }
    }
    public enum playerSpeed { FAST, MEDIUM, SLOW }
    public void ChangePlayerUpdateRate(playerSpeed playerSpeed)
    {
        switch (playerSpeed)
        {
            case playerSpeed.FAST:
                playerUpdateRate = 2;
                break;
            case playerSpeed.MEDIUM:
                playerUpdateRate = 4;
                break;
            case playerSpeed.SLOW:
                playerUpdateRate = 8;
                break;
            default:
                break;
        }
    }
}
