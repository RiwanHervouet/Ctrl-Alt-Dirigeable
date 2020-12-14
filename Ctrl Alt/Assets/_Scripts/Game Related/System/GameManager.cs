using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Initialization
    public OOE_Map mapScript;
    public Player player;
    public ReadSouffleur souffleur;
    [Range(0f, 4f)] public float inputSelectionTime = 1f;
    [HideInInspector] public bool canReceiveInput = true;
    [HideInInspector] public int cleanInputList = 0;

    [Header("Player Data")]
    public int xPlayerSpawn;
    public int yPlayerSpawn;

    [Header("Gameplay Tweaks")]
    [SerializeField] [Tooltip("Number of updates the player is invincible")] [Range(0, 10)] private int invincibilityDelay= 4;
    [SerializeField] [Tooltip("Number of updates the player can't be pushed by wind again")] [Range(0, 5)] private int windPassiveDelay = 0;

    public enum altitudes { TopAltitude, MiddleAltitude, BottomAltitude }
    public altitudes currentAltitude;
    public altitudes startAltitude = altitudes.MiddleAltitude;

    #region do not touch stuff
    [HideInInspector] public int InvincibilityDelay { get { return invincibilityDelay; } }
    [HideInInspector] public int WindPassiveDelay { get { return windPassiveDelay; } }
    #endregion 
    #endregion 

    void Init()
    {
        if (!mapScript)
        {
            mapScript = FindObjectOfType<OOE_Map>();
        }
        if (!player)
        {
            player = FindObjectOfType<Player>();
        }
        if (!souffleur)
        {
            souffleur = FindObjectOfType<ReadSouffleur>();
        }

        currentAltitude = startAltitude;
    }

    private void Start()
    {
        Init();
    }
}
