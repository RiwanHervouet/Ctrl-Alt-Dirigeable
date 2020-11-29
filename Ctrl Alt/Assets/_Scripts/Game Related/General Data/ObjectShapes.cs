using System.Collections;
using UnityEngine;

public enum allObjectShapes { mountainClassic, NULL }
public enum allAnimations { lightningPrep, NULL }
public static class ObjectShapes
{
    #region Physical Objects
    public static Vector2[] mountainClassic =
    {
        new Vector2(-2f, -1f), new Vector2(-3f, -1f),
        new Vector2(-3f, 0f), new Vector2(-3f, 1f),
        new Vector2(-2f, 1f), new Vector2(-2f, 2f),
        new Vector2(-1f, 2f), new Vector2(-1f, 3f),
        new Vector2(0f, 3f), new Vector2(1f, 3f),
        new Vector2(1f, 2f), new Vector2(2f, 2f),
        new Vector2(2f, 1f), new Vector2(3f, 1f),
        new Vector2(3f, 0f), new Vector2(3f, -1f),
        new Vector2(2f, -1f), new Vector2(2f, -2f),
        new Vector2(1f, -2f), new Vector2(1f, -3f),
        new Vector2(0f, -3f), new Vector2(-1f, -3f),
        new Vector2(-1f, -2f), new Vector2(-2f, -2f)
    };
    #endregion

    #region Immaterial Objects 
    public static Vector2[] lightningPrepAnimation(int step)
    {
        switch (step)
        {
            case 0:
                return lightningPrepAnimation1;
            case 1:
                return lightningPrepAnimation2;
            default:
                Debug.LogError("L'animation demandée n'existe pas.");
                return new Vector2[] { Vector2.zero };
        }
    }
    public static Vector2[] lightningPrepAnimation1 =
    {
        new Vector2(0,0)
    };
    public static Vector2[] lightningPrepAnimation2 =
    {
        new Vector2(1,0)
    };
    #endregion  
}
