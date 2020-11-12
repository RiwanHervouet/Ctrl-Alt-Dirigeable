using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum physicalObjectType { mountain, player, lightning, NULL }
public enum immaterialObjectType { underMountain, wind, lightningPrep, storm, NULL }

public class MapObject : MonoBehaviour
{
    #region Initialization
    public int xPosition = 0;
    public int yPosition = 0;

    public physicalObjectType physicalType = physicalObjectType.NULL;
    public immaterialObjectType immaterialType = immaterialObjectType.NULL;

    public Vector2[] graphics;
    [Tooltip("Draw Squares between top left point xy and bottom right point xy")]
    public Vector2[] graphicsRange;

    public Vector2 nextRelativePosition { 
        get { return _nextRelativePosition; } 
        set { if (physicalType == physicalObjectType.player) _nextRelativePosition = value; } }

    #region Code related
    private Vector2[] lastGraphics;
    private int totalDotsDisplayed;
    private List<Vector2> tempList;
    private Vector2 _nextRelativePosition = Vector2.zero;
    #endregion
    #endregion

    #region Constructors
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;
    }
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType, Vector2 direction)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;

        nextRelativePosition = direction;
    }
    #endregion

    protected virtual void InitObject()
    {
        if (physicalType != physicalObjectType.player)
        {
            GameEvents.Instance.OnNextEnvironmentUpdate += UpdateObject;
        }
        else
        {
            GameEvents.Instance.OnNextPlayerUpdate += UpdateObject;
        }
        totalDotsDisplayed = 0;
        tempList = new List<Vector2>();
    }
    protected virtual void DisableObject() { }

    void UpdateObjectPosition()
    {
        xPosition += (int)nextRelativePosition.x;
        yPosition += (int)nextRelativePosition.y;
    }

    protected virtual void UpdateObject()
    {
        ClearObjectVisuals();
        UpdateObjectPosition();
        DisplayObjectVisuals();
    }

    private void ClearObjectVisuals()
    {
        if (lastGraphics != null)
        {
            for (int i = 0; i < lastGraphics.Length; i++)
            {
                GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].physicalObjectOnMe.Remove(physicalType);
            }
        }
    }
    private void DisplayObjectVisuals()
    {
        totalDotsDisplayed = 0;
        tempList.Clear();

        for (int i = 0; i < graphics.Length; i++)
        {
            if (CoordinateIsWithinTheMap(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y))
            {
                GameManager.Instance.mapScript.fullMap[xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y].physicalObjectOnMe.Add(physicalType);
                totalDotsDisplayed++;
                tempList.Add(new Vector2(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y));
            }
            else
            {
                if (physicalType == physicalObjectType.player)
                {
                    GameEvents.Instance.PlayerIsHit(new List<physicalObjectType> { physicalObjectType.NULL });
                }
            }
        }
        for (int k = 0; k < graphicsRange.Length * 0.5f; k++)
        {
            for (int j = 0; j < graphicsRange[k * 2 + 1].y - graphicsRange[k * 2].y; j++)
            {
                for (int i = 0; i < graphicsRange[k * 2 + 1].x - graphicsRange[k * 2].x; i++)
                {
                    if (CoordinateIsWithinTheMap(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j))
                    {
                        if (GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].physicalObjectOnMe.Contains(physicalType) == false)
                        {
                            GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].physicalObjectOnMe.Add(physicalType);
                            totalDotsDisplayed++;
                            tempList.Add(new Vector2(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j));
                        }
                    }
                }
            }
        }
        lastGraphics = new Vector2[totalDotsDisplayed];
        for (int i = 0; i < totalDotsDisplayed; i++)
        {
            lastGraphics[i] = tempList[i];
        }
    }

    #region Set up event related
    private void Start()
    {
        InitObject();
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnNextEnvironmentUpdate -= UpdateObject;
            DisableObject();
        }
    }
    #endregion

    bool CoordinateIsWithinTheMap(int xCoordinate, int yCoordinate)
    {
        if (xCoordinate > 0)

            if (xCoordinate < GameManager.Instance.mapScript.fullMap.GetLength(0))

                if (yCoordinate > 0)

                    if (yCoordinate < GameManager.Instance.mapScript.fullMap.GetLength(1))

                        return true;

        return false;
    }
}
