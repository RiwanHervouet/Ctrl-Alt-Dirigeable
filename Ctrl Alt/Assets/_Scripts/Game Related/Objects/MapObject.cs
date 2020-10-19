using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum objectType { mountain,player,storm,lightning }

public class MapObject : MapEntity
{
    public objectType type;

    public Vector2[] graphics;

    public Vector2 nextRelativePosition;

    #region Constructors
    public MapObject(int xPosition, int yPosition, Image image, Vector2[] graphics) : base(xPosition, yPosition, image)
    {
        this.graphics = graphics;
    }

    public MapObject(Image image) : base(image)
    {

    }
    #endregion
}
