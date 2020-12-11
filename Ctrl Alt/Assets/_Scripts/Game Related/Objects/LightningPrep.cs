using System.Collections;
using UnityEngine;

public class LightningPrep : MonoBehaviour
{
    #region Initialization
    MapObject lightningPrep;
    #endregion
    //////////////////////// S'occupe d'afficher les bonnes cases au lightning prep au dessus et de le faire s'actualiser selon les refreshs
    void Start()
    {
        lightningPrep = lightningPrep ? lightningPrep : GetComponent<MapObject>();
    }

    void Update()
    {
        lightningPrep.graphicsMiddleAltitude = new Vector2[] { new Vector2(/*plein de belles cases*/0, 0) };
    }
}
