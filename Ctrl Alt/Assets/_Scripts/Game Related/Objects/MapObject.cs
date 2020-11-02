using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum objectType { mountain, player, storm, lightning, nothing }

public class MapObject
{
    public int xPosition = 0;
    public int yPosition = 0;

    public objectType type;

    public Vector2[] graphics;

    public Vector2 nextRelativePosition;

    public Color myColor;
    /*
    {
        get { return mapEntity.myColor; }
        set { mapEntity.myColor = value; }
    }
    */

    private MapEntity mapEntity;



    #region Constructors
    public MapObject(int xPositionInit, int yPositionInit, objectType objectType)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        this.type = objectType;

        switch (objectType)
        {
            case objectType.mountain:
                break;
            case objectType.player:
                InitPlayer();
                break;
            case objectType.storm:
                break;
            case objectType.lightning:
                break;
            default:
                break;
        }
    }
    #endregion

    void InitObject()
    {

    }

    void InitPlayer()
    {
        graphics = new Vector2[] { new Vector2(-1, 0), new Vector2(-2, 0) };
    }

    void UpdateEnvironmentObject()
    {
        xPosition += (int)nextRelativePosition.x;
        yPosition += (int)nextRelativePosition.y;
    }

    public void UpdateObject()
    {
        //OOE_Map
    }
}
