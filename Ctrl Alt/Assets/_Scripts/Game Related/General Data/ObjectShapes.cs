using System.Collections;
using UnityEngine;

public enum allObjectShapes { mountainClassic, NULL }
public static class ObjectShapes
{

    #region Physical Objects
    public static Vector2[] mountainClassic =
    {
        new Vector2(-2f, -1f),
        new Vector2(-3f, -1f),
        new Vector2(-3f, 0f),
        new Vector2(-3f, 1f),
        new Vector2(-2f, 1f),
        new Vector2(-2f, 2f),
        new Vector2(-1f, 2f),
        new Vector2(-1f, 3f),
        new Vector2(0f, 3f),
        new Vector2(1f, 3f),
        new Vector2(1f, 2f),
        new Vector2(2f, 2f),
        new Vector2(2f, 1f),
        new Vector2(3f, 1f),
        new Vector2(3f, 0f),
        new Vector2(3f, -1f),
        new Vector2(2f, -1f),
        new Vector2(2f, -2f),
        new Vector2(1f, -2f),
        new Vector2(1f, -3f),
        new Vector2(0f, -3f),
        new Vector2(-1f, -3f),
        new Vector2(-1f, -2f),
        new Vector2(-2f, -2f)
    };
    #endregion
}
