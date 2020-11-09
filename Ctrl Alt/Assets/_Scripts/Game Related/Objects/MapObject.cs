using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum objectType { mountain, player, lightning, NULL }

public class MapObject : MonoBehaviour
{
    public int xPosition = 0;
    public int yPosition = 0;

    public objectType type;

    public Vector2[] graphics;
    [Tooltip("Draw Squares between first point xy and second point xy")]
    public Vector2[] graphicsRange;
    private Vector2[] lastGraphics;
    private int totalDotsDisplayed;
    private List<Vector2> tempList;

    public Vector2 nextRelativePosition { get { return _nextRelativePosition; } set { if (type == objectType.player) _nextRelativePosition = value; } }
    private Vector2 _nextRelativePosition = Vector2.zero;

    #region Constructors
    public MapObject(int xPositionInit, int yPositionInit, objectType objectType)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        type = objectType;
    }
    public MapObject(int xPositionInit, int yPositionInit, objectType objectType, Vector2 direction)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        type = objectType;

        nextRelativePosition = direction;
    }
    #endregion

    protected virtual void InitObject()
    {
        GameEvents.Instance.OnNextEnvironmentUpdate += UpdateObject;
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
            for (int i = 0; i < graphics.Length; i++)
            {
                GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].objectVisualOnMe.Remove(type);
            }
        }
    }
    private void DisplayObjectVisuals()
    {
        totalDotsDisplayed = 0;
        tempList.Clear();
        for (int i = 0; i < graphics.Length; i++)
        {
            GameManager.Instance.mapScript.fullMap[xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y].objectVisualOnMe.Add(type);
            totalDotsDisplayed++;
        }
        for (int k = 0; k < graphicsRange.Length * 0.5f; k++)
        {
            for (int j = 0; j < graphicsRange[k * 2 + 1].y - graphicsRange[k * 2].x; j++)
            {
                for (int i = 0; i < graphicsRange[k * 2 + 1].x - graphicsRange[k * 2].x; i++)
                {
                    if (GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].objectVisualOnMe.Contains(type) == false)
                    {
                        GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].objectVisualOnMe.Add(type);
                        totalDotsDisplayed++;
                        tempList.Add(new Vector2(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j));
                    }
                }
            }
        }
        lastGraphics = new Vector2[totalDotsDisplayed];
        for (int i = 0; i < totalDotsDisplayed; i++)
        {
            if (i < graphics.Length)
            {
                lastGraphics[i] = new Vector2(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y);
            }
            else
            {
                lastGraphics[i] = tempList[i - graphics.Length];
            }
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
}
