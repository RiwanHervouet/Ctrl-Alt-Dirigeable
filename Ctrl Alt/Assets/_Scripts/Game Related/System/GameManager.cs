using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public OOE_Map mapScript;

    [Header("Player Data")]
    public int xPlayerSpawn;
    public int yPlayerSpawn;

    [Tooltip("Number of updates the player is invincible")][Range(0,10)]public int invincibilityDelay = 4;

    void Init()
    {
        //fullMap = getComponent....
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        
    }
}
