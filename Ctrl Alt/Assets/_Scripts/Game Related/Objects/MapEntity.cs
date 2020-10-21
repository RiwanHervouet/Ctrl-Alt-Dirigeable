using UnityEngine;
using UnityEngine.UI;

public class MapEntity
{
    //public bool objectOnMe = false; //pour ne pas afficher ce map entity (désactiver l'image) ou faire d'autres choses

    public Color myColor 
    { 
        get { return myImage.color; }
        set { myImage.color = value; }
    }

    private Color baseColor = new Color(Mathf.InverseLerp(0, 255, 139), Mathf.InverseLerp(0, 255, 69), Mathf.InverseLerp(0, 255, 19)); //brown

    private Image myImage;

    #region Constructors
    public MapEntity (Image image)
    {
        myImage = image;
    }
    public MapEntity (Image image, Color baseColor)
    {
        myImage = image;
        this.baseColor = baseColor;
    }
    #endregion
}
