using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEntity
{
    public List<MapObject> objectOnMe = new List<MapObject>();

    //public bool inAStorm = false;
    //public int edgeOfTheMap = false;

    //public bool objectOnMe = false; //pour ne pas afficher ce map entity (désactiver l'image) ou faire d'autres choses

    public Color myColor 
    { 
        get { return myImage.color; }
        set { myImage.color = value; }
    }

    private Color _baseColor = Colors.background1; //brown

    public Color BaseColor { get { return _baseColor; } }

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
    #endregion
}
