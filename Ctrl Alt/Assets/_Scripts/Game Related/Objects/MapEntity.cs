using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEntity
{
    public List<objectType> objectVisualOnMe = new List<objectType>();

    public bool inAStorm = false;

    public Color myColor 
    { 
        get { return myImage.color; }
        set { myImage.color = value; }
    }

    private Color _baseColor = Colors.background1;

    public Color BaseColor { get { return _baseColor; } }

    private int _edgeOfMapDistance = 1000;
    public int edgeOfMapDistance { get { return _edgeOfMapDistance; } }
    private Image myImage;

    #region Constructors
    public MapEntity (Image image)
    {
        myImage = image;
    }
    public MapEntity (Image image, Color baseColor)
    {
        myImage = image;
        this._baseColor = baseColor;
    }
    public MapEntity (Image image, Color baseColor, int edgeOfMapDistance)
    {
        myImage = image;
        _baseColor = baseColor;
        _edgeOfMapDistance = edgeOfMapDistance;
    }
    #endregion

    /*public void ResetEnvironmentInfo()
    {
        for (int i = 0; i < objectVisualOnMe.Count; i++)
        {
            if (objectVisualOnMe[i] != objectType.player)
            {
                objectVisualOnMe.RemoveAt(i);
                i--;
            }
        }
        inAStorm = false;
    }
    public void ResetPlayerInfo()
    {
        for (int i = 0; i < objectVisualOnMe.Count; i++)
        {
            if(objectVisualOnMe[i] == objectType.player)
            {
                objectVisualOnMe.RemoveAt(i);
                i--;
                break;
            }
        }
    }*/
}
