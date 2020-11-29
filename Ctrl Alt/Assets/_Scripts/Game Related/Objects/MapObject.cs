using System;
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
    public immaterialObjectType immaterialType { get { return immaterialTypeComplete[0]; } set { immaterialTypeComplete[0] = value; } }
    //array pour les animations, immaterialTypeComplete[0] est le nombre d'éléments en + du tableau sont la valeur d'alpha à sélectionner
    public immaterialObjectType[] immaterialTypeComplete = new immaterialObjectType[] { immaterialObjectType.NULL };

    public Vector2[] graphics;
    [Tooltip("Draw Squares between top left xy point and bottom right xy point")]
    public Vector2[] graphicsRange;

    public Vector2 nextRelativePosition
    {
        get { return _nextRelativePosition; }
        set { if (physicalType == physicalObjectType.player) _nextRelativePosition = value; }
    }

    public Vector2 windDirection = Vector2.zero;

    #region Code related
    private bool alreadyPushedPlayer = false;
    private int windPassiveDelay = 0;
    private Vector2[] lastGraphics;
    private int totalDotsDisplayed;
    private List<Vector2> tempList;
    private Vector2 _nextRelativePosition = Vector2.zero;
    #endregion
    #endregion

    #region Constructors
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;
        immaterialType = immaterialObjectType.NULL;

        this.graphics = graphics;
        this.graphicsRange = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, immaterialObjectType objectType, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        immaterialType = objectType;
        physicalType = physicalObjectType.NULL;

        this.graphics = graphics;
        this.graphicsRange = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, physicalObjectType objectType, Vector2 direction, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        physicalType = objectType;
        immaterialType = immaterialObjectType.NULL;

        nextRelativePosition = direction;

        this.graphics = graphics;
        this.graphicsRange = graphicsRange;
    }
    public MapObject(int xPositionInit, int yPositionInit, immaterialObjectType objectType, Vector2 direction, Vector2[] graphics, params Vector2[] graphicsRange)
    {
        xPosition = xPositionInit;
        yPosition = yPositionInit;

        immaterialType = objectType;
        physicalType = physicalObjectType.NULL;

        nextRelativePosition = direction;

        this.graphics = graphics;
        this.graphicsRange = graphicsRange;
    }
    #endregion

    protected virtual void InitObject()
    {
        Debug.Log("je suis "+ physicalType +" "+immaterialType, this);  
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
        }
        totalDotsDisplayed = 0;
        tempList = new List<Vector2>();
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

        for (int i = 0; i < graphics.Length; i++)
        {
            if (CoordinateIsWithinTheMap(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y))
            {
                if (physicalType != physicalObjectType.NULL)
                {
                    GameManager.Instance.mapScript.fullMap[xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y].physicalObjectOnMe.Add(physicalType);
                    totalDotsDisplayed++;
                    tempList.Add(new Vector2(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y));
                }
                if (immaterialType != immaterialObjectType.NULL)
                {
                    GameManager.Instance.mapScript.fullMap[xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y].immaterialObjectOnMe.Add(immaterialType);
                    totalDotsDisplayed++;
                    tempList.Add(new Vector2(xPosition + (int)graphics[i].x, yPosition + (int)graphics[i].y));
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
        for (int k = 0; k < graphicsRange.Length * 0.5f; k++)
        {
            for (int j = 0; j < graphicsRange[k * 2 + 1].y - graphicsRange[k * 2].y; j++)
            {
                for (int i = 0; i < graphicsRange[k * 2 + 1].x - graphicsRange[k * 2].x; i++)
                {
                    if (CoordinateIsWithinTheMap(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j))
                    {
                        if (physicalType != physicalObjectType.NULL)
                        {
                            if (GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].physicalObjectOnMe.Contains(physicalType) == false)
                            {
                                GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].physicalObjectOnMe.Add(physicalType);
                                totalDotsDisplayed++;
                                tempList.Add(new Vector2(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j));
                            }
                        }
                        if (immaterialType != immaterialObjectType.NULL)
                        {
                            if (GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].immaterialObjectOnMe.Contains(immaterialType) == false)
                            {
                                GameManager.Instance.mapScript.fullMap[xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j].immaterialObjectOnMe.Add(immaterialType);
                                totalDotsDisplayed++;
                                tempList.Add(new Vector2(xPosition + (int)graphicsRange[k * 2].x + i, yPosition + (int)graphicsRange[k * 2].y + j));
                            }
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

    Vector2[] GetShape (allObjectShapes shape)
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
