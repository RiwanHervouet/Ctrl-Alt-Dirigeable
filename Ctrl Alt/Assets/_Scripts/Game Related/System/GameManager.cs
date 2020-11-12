using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public OOE_Map mapScript;
    [Range(0f, 4f)] public float inputSelectionTime = 1f;
    [HideInInspector] public bool canReceiveInput = true;

    [Header("Player Data")]
    public int xPlayerSpawn;
    public int yPlayerSpawn;

    [Tooltip("Number of updates the player is invincible")] [Range(0, 10)] public int invincibilityDelay = 4;

    void Init()
    {
        if (!mapScript)
        {
            mapScript = FindObjectOfType<OOE_Map>();
        }
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {

    }
}
