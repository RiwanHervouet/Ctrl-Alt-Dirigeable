using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum physicalObjectType { NULL, mountain, player, lightning }
public enum immaterialObjectType { NULL, underMountain, wind, lightningPrep, storm }

public class MapObject : MonoBehaviour
{
    #region Initialization
    public int xPosition = 0;
    public int yPosition = 0;

    public physicalObjectType physicalType = physicalObjectType.NULL;
    public immaterialObjectType immaterialType = immaterialObjectType.NULL;
    [Range(0, 5)] public int additionalAlphaIndex = 0;

    public Vector2[] graphicsMiddleAltitude;
    [Tooltip("Draw Squares between top left xy point and bottom right xy point")]
    public Vector2[] graphicsRangeMiddleAltitude;

    [Header("Special cases")]
    public Vector2[] graphicsTopAltitude;
    [Tooltip("Draw Squares between top left xy point and bottom right xy point")]
    public Vector2[] graphicsRangeTopAltitude;

    public Vector2[] graphicsBottomAltitude;
    [Tooltip("Draw Squares between top left xy point and bottom right xy point")]
    public Vector2[] graphicsRangeBottomAltitude;

    public Vector2 nextRelativePosition
    {
        get { return _nextRelativePosition; }
        set { if (physicalType == physicalObjectType.player) _nextRelativePosition = value; }
    }
    [Space]
    public Vector2 windDirection = Vector2.zero;

    #region Code related
    private bool alreadyPushedPlayer = false;
    private int windPassiveDelay = 0;
    private Vector2[] lastGraphics;
    private int totalDotsDisplayed;
    private List<Vector2> tempList;
    private Vector2 _nextRelativePosition = Vector2.zero;
    private GameManager.altitudes currentAltitude;
    private Vector2[] currentGraphicsAltitude;
    private Vector2[] currentGraphicsRangeAltitude;

    private MapObject underObject;
    #endregion
    #endregion

    #region Constructors
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;
        immaterialType = immaterialObjectType.NULL;

        this.graphicsMiddleAltitude = graphics;
        this.graphicsRangeMiddleAltitude = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, immaterialObjectType objectType, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        immaterialType = objectType;
        physicalType = physicalObjectType.NULL;

        this.graphicsMiddleAltitude = graphics;
        this.graphicsRangeMiddleAltitude = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType, Vector2 direction, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;
        immaterialType = immaterialObjectType.NULL;

        nextRelativePosition = direction;

        this.graphicsMiddleAltitude = graphics;
        this.graphicsRangeMiddleAltitude = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, immaterialObjectType objectType, Vector2 direction, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        immaterialType = objectType;
        physicalType = physicalObjectType.NULL;

        nextRelativePosition = direction;

        this.graphicsMiddleAltitude = graphics;
        this.graphicsRangeMiddleAltitude = graphicsRange;
    }
    public MapObject(bool tisThisConstructor, int xPositionInit, int yPositionInit, immaterialObjectType objectType, Vector2[] topGraphics, Vector2[] middleGraphics, Vector2[] bottomGraphics, Vector2[] topGraphicsRange, Vector2[] middleGraphicsRange, Vector2[] bottomGraphicsRange)
    {
        bool variableInutile = tisThisConstructor;
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        immaterialType = objectType;
        physicalType = physicalObjectType.NULL;

        graphicsTopAltitude = topGraphics;
        graphicsMiddleAltitude = middleGraphics;
        graphicsBottomAltitude = bottomGraphics;

        graphicsRangeTopAltitude = topGraphicsRange;
        graphicsRangeMiddleAltitude = middleGraphicsRange;
        graphicsRangeBottomAltitude = bottomGraphicsRange;
    }
    #endregion

    protected virtual void InitObject()
    {
        if (physicalType != physicalObjectType.player)
        {
            GameEvents.Instance.OnNextEnvironmentUpdate += UpdateObject;

            if (immaterialType == immaterialObjectType.wind)
            {
                GameEvents.Instance.OnNextEnvironmentUpdate += Wind;
            }
        }
        else
        {
            GameEvents.Instance.OnNextPlayerUpdate += UpdateObject;

            if (physicalType == physicalObjectType.mountain)
            {
                underObject = new MapObject(true, xPosition, yPosition, immaterialObjectType.underMountain, graphicsMiddleAltitude, graphicsBottomAltitude, new Vector2[0], graphicsRangeMiddleAltitude, graphicsRangeBottomAltitude, new Vector2[0]);
            }
        }
        totalDotsDisplayed = 0;
        tempList = new List<Vector2>();

        //currentAltitude = GameManager.Instance.startAltitude;
    }
    protected virtual void DisableObject() { }

    void UpdateObjectPosition()
    {
        if (windPassiveDelay < 0)
        {
            alreadyPushedPlayer = false;
            xPosition += (int)nextRelativePosition.x;
            yPosition += (int)nextRelativePosition.y;
        }
        else
        {
            windPassiveDelay--;
        }
    }

    protected virtual void UpdateObject()
    {
        ClearObjectVisuals();
        UpdateObjectPosition();
        DisplayObjectVisuals();
    }

    protected void ClearObjectVisuals()
    {
        if (lastGraphics != null)
        {
            for (int i = 0; i < lastGraphics.Length; i++)
            {
                if (GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].physicalObjectOnMe.Contains(physicalType))
                {
                    GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].physicalObjectOnMe.Remove(physicalType);
                }
                if (GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].immaterialObjectOnMe.Contains(immaterialType))
                {
                    GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].immaterialObjectOnMe.Remove(immaterialType);
                }
            }
        }
    }
    protected void DisplayObjectVisuals()
    {
        totalDotsDisplayed = 0;
        tempList.Clear();

        if (currentAltitude != GameManager.Instance.currentAltitude)
        {
            if (GameManager.Instance.currentAltitude == GameManager.altitudes.MiddleAltitude || (graphicsTopAltitude.Length == 0 && graphicsRangeTopAltitude.Length == 0 && graphicsBottomAltitude.Length == 0 && graphicsRangeBottomAltitude.Length == 0))
            {
                currentGraphicsAltitude = graphicsMiddleAltitude;
                currentGraphicsRangeAltitude = graphicsRangeMiddleAltitude;
                currentAltitude = GameManager.Instance.currentAltitude;
            }
            else if (GameManager.Instance.currentAltitude == GameManager.altitudes.BottomAltitude)
            {
                currentGraphicsAltitude = graphicsBottomAltitude;
                currentGraphicsRangeAltitude = graphicsRangeBottomAltitude;
                currentAltitude = GameManager.Instance.currentAltitude;
            }
            else
            {
                currentGraphicsAltitude = graphicsTopAltitude;
                currentGraphicsRangeAltitude = graphicsRangeTopAltitude;
                currentAltitude = GameManager.Instance.currentAltitude;
            }
        }

        for (int i = 0; i < currentGraphicsAltitude.Length; i++)
        {
            if (CoordinateIsWithinTheMap(xPosition + (int)currentGraphicsAltitude[i].x, yPosition + (int)currentGraphicsAltitude[i].y))
            {
                if (physicalType != physicalObjectType.NULL)
                {
                    GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsAltitude[i].x, yPosition + (int)currentGraphicsAltitude[i].y].physicalObjectOnMe.Add(physicalType);
                    totalDotsDisplayed++;
                    tempList.Add(new Vector2(xPosition + (int)currentGraphicsAltitude[i].x, yPosition + (int)currentGraphicsAltitude[i].y));
                }
                if (immaterialType != immaterialObjectType.NULL)
                {
                    GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsAltitude[i].x, yPosition + (int)currentGraphicsAltitude[i].y].immaterialObjectOnMe.Add(immaterialType);
                    totalDotsDisplayed++;
                    tempList.Add(new Vector2(xPosition + (int)currentGraphicsAltitude[i].x, yPosition + (int)currentGraphicsAltitude[i].y));
                }
            }
            else
            {
                if (physicalType == physicalObjectType.player)
                {
                    GameEvents.Instance.PlayerIsHit(new List<physicalObjectType> { physicalObjectType.NULL });
                }
            }
        }
        for (int k = 0; k < currentGraphicsRangeAltitude.Length * 0.5f; k++)
        {
            for (int j = 0; j < currentGraphicsRangeAltitude[k * 2 + 1].y - currentGraphicsRangeAltitude[k * 2].y; j++)
            {
                for (int i = 0; i < currentGraphicsRangeAltitude[k * 2 + 1].x - currentGraphicsRangeAltitude[k * 2].x; i++)
                {
                    if (CoordinateIsWithinTheMap(xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j))
                    {
                        if (physicalType != physicalObjectType.NULL)
                        {
                            if (GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j].physicalObjectOnMe.Contains(physicalType) == false)
                            {
                                GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j].physicalObjectOnMe.Add(physicalType);
                                totalDotsDisplayed++;
                                tempList.Add(new Vector2(xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j));
                            }
                        }
                        if (immaterialType != immaterialObjectType.NULL)
                        {
                            if (GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j].immaterialObjectOnMe.Contains(immaterialType) == false)
                            {
                                GameManager.Instance.mapScript.fullMap[xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j].immaterialObjectOnMe.Add(immaterialType);
                                totalDotsDisplayed++;
                                tempList.Add(new Vector2(xPosition + (int)currentGraphicsRangeAltitude[k * 2].x + i, yPosition + (int)currentGraphicsRangeAltitude[k * 2].y + j));
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
            if (physicalType != physicalObjectType.player)
            {
                GameEvents.Instance.OnNextEnvironmentUpdate -= UpdateObject;

                if (immaterialType == immaterialObjectType.wind)
                {
                    GameEvents.Instance.OnNextEnvironmentUpdate -= Wind;
                }
            }
            else
            {
                GameEvents.Instance.OnNextPlayerUpdate -= UpdateObject;
            }
            DisableObject();
        }
    }
    #endregion

    void Wind()
    {
        Debug.Log("weow");
        for (int i = 0; i < lastGraphics.Length; i++)
        {
            GameManager.Instance.mapScript.fullMap[(int)lastGraphics[i].x, (int)lastGraphics[i].y].wind = windDirection; //pour les animations de vent
            if (lastGraphics[i] == new Vector2(GameManager.Instance.player.xPosition, GameManager.Instance.player.yPosition) && !alreadyPushedPlayer)
            {
                Debug.Log("Vent est censé faire action");
                GameManager.Instance.player.xPosition += (int)windDirection.x;
                GameManager.Instance.player.yPosition += (int)windDirection.y;
                alreadyPushedPlayer = true;
                windPassiveDelay = GameManager.Instance.WindPassiveDelay;
            }
        }
    }

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

[Serializable]
public class MapObjectConstructor// : ScriptableObject
{
    [Header("Type d'objet :")]
    public physicalObjectType physicalObjectType = physicalObjectType.NULL;
    public immaterialObjectType immaterialObjectType = immaterialObjectType.NULL;

    [Header("Position lors du spawn :")]
    public bool dependingOnPlayerPosition = false;
    public int xSpawnPosition;
    public int ySpawnPosition;

    [Header("Désigne le mouvement qu'il fera chaque fois qu'il est update :")]
    [Range(-1, 1)] public int xDirection = 0;
    [Range(-1, 1)] public int yDirection = 0;

    [Header("Visuels :")]
    public allObjectShapes shape = allObjectShapes.NULL;
    public Vector2[] additionalZone;

    [Header("Circumstancial options :")]
    [Range(-1, 1)] public int xWindDirection = 0;
    [Range(-1, 1)] public int yWindDirection = 0;
    public void Init()
    {
        if (dependingOnPlayerPosition)
        {
            xSpawnPosition += GameManager.Instance.player.xPosition;
            ySpawnPosition += GameManager.Instance.player.yPosition;
        }
        if (physicalObjectType != physicalObjectType.NULL)
        {
            if (physicalObjectType == physicalObjectType.player)
                Debug.LogError("Attention hein, je te spawn le joueur mais tu fais pas n'importe quoi avec hein !");
            new MapObject(xSpawnPosition, ySpawnPosition, physicalObjectType, new Vector2(xDirection, yDirection), GetShape(shape), additionalZone);
        }
        else if (immaterialObjectType != immaterialObjectType.NULL)
        {
            new MapObject(xSpawnPosition, ySpawnPosition, immaterialObjectType, new Vector2(xDirection, yDirection), GetShape(shape), additionalZone);
        }
        else
        {
            Debug.LogError("L'objet créé n'a pas de type, il n'existe donc pas.");
        }
    }

    Vector2[] GetShape(allObjectShapes shape)
    {
        switch (shape)
        {
            case allObjectShapes.mountainClassic:
                return ObjectShapes.mountainClassic;
            default:
                Debug.LogError("Il faut ajouter la forme ici avant qu'elle puisse être affichée.");
                return new Vector2[] { Vector2.zero };
        }
    }
}
