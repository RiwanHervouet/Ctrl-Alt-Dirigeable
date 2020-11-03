using System;
using UnityEngine;

public static class Colors
{
    public static Color background1 { get { return GetColor(139, 69, 19); } } // dark brown
    public static Color background2 { get { return GetColor(160, 82, 45); } } // light brown
    public static Color player { get { return GetColor(255, 255, 255); } } // white
    public static Color mountain { get { return GetColor(138, 219, 147); } } // greenish
    public static Color storm { get { return GetColor(133, 133, 133); } } // grey
    public static Color outOfBounds { get { return GetColor(186, 40, 40); } } // red
    public static Color lightning { get { return GetColor(115, 224, 255); } } // sky blue




    // Some alpha values (percentage)
    public static float stormAlpha { get { return 0.65f; } }
    public static float[] almostOutOfBounds { get { return new float[] { 0.8f, 0.6f, 0.4f, 0.2f }; } } // Number of values is the number of cells warning the player he is near the edge of the map.
    public static float[] subAltitude { get { return new float[] { 0.5f }; } }



    #region color methods
    public static Color GetColor(float red, float green, float blue)
    {
        return new Color(Mathf.InverseLerp(0, 255, red), Mathf.InverseLerp(0, 255, green), Mathf.InverseLerp(0, 255, blue));
    }
    public static Color GetColor(float red, float green, float blue, float alpha)
    {
        return new Color(Mathf.InverseLerp(0, 255, red), Mathf.InverseLerp(0, 255, green), Mathf.InverseLerp(0, 255, blue), Mathf.InverseLerp(0, 255, alpha));
    }
    #endregion
}
