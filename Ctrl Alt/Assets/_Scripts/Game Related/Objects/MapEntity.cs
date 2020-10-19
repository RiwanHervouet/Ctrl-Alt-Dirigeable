using UnityEngine;
using UnityEngine.UI;

public class MapEntity
{
    public int xPosition = 0;
    public int yPosition = 0;

    public Color myColor 
    { 
        get { return myImage.color; }
        set { myImage.color = value; }
    }

    private Image myImage;

    #region Constructors
    public MapEntity (int xPosition,int yPosition, Image image)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;

        myImage = image;
    }
    public MapEntity(Image image)
    {
        myImage = image;
    }
    #endregion
}
