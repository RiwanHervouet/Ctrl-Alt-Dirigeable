using UnityEngine;
using UnityEngine.UIElements;

public enum objectType { mountain, player, lightning, NULL }

public class MapObject : MonoBehaviour
{
    public int xPosition = 0;
    public int yPosition = 0;

    public objectType type;

    public Vector2[] graphics;
    private Vector2[] lastGraphics;

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
        for (int i = 0; i < graphics.Length; i++)
        {
            GameManager.Instance.mapScript.fullMap[xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y].objectVisualOnMe.Add(type);
        }
        lastGraphics = new Vector2[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            lastGraphics[i] = new Vector2(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y);
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
