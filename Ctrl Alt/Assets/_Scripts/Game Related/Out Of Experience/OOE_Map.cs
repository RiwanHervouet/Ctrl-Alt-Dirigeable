using System;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class OOE_Map : MonoBehaviour
{
    #region Initiatlization

    #region Inhérents au jeu
    //public GameObject player;
    //public Player playerMO;
    public Vector2 matrixTopLeftCoordinate;
    #endregion

    #region Map Characteristics to become Scriptable Object après les LD elements
    [Header("Map Characteristics")]
    [Tooltip("La matrice fait 32x32")]
    public MapEntity[,] fullMap;
    #endregion

    #region LD elements to become Scriptable Object
    /*[Header("LD elements")]
    public Vector2 spawnPlayer;
    public GameObject mountain;
    public GameObject storm;
    public GameObject missile; // ou sinon des éclairs (ronds qui se rapprochent jusqu'à frapper une zone fortement quand ils sont tous sur le même point (la déflagration))
    */
    #endregion

    Transform tempParent;
    Color tempColor;
    bool underMountainWasDisplayed = false;
    int currentHorizontalIndex = 0;
    int currentVerticalIndex = 0;

    #endregion

    void Start()
    {
        if (transform.childCount < 2) Debug.LogWarning("The map must be child of this object");
        fullMap = new MapEntity[transform.GetChild(0).childCount, transform.childCount];

        for (int j = 0; j < fullMap.GetLength(1); j++)
        {
            tempParent = transform.GetChild(j).transform;
            for (int i = 0; i < fullMap.GetLength(0); i++)
            {
                fullMap[i, j] = new MapEntity(
                        tempParent.GetChild(i).GetComponent<Image>(),
                        Mathf.FloorToInt(i * 0.125f - j * .015625f) % 2 == 0 ? Colors.background1 : Colors.background2,
                        DistanceToEdgeOfMap(i, j)
                        );
            }
        }
    }
    void OnEnable()
    {
        GameEvents.Instance.OnNextRefresh += NextRefresh;
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnNextRefresh -= NextRefresh;
        }
    }

    private void NextRefresh()
    {
        for (currentVerticalIndex = 0; currentVerticalIndex < fullMap.GetLength(1); currentVerticalIndex++)
        {
            for (currentHorizontalIndex = 0; currentHorizontalIndex < fullMap.GetLength(0); currentHorizontalIndex++)
            {
                fullMap[currentHorizontalIndex, currentVerticalIndex].myColor = DisplayRightColor(fullMap[currentHorizontalIndex, currentVerticalIndex]);
                Feedback(fullMap[currentHorizontalIndex, currentVerticalIndex]);
            }
        }
    }

    private void Feedback(MapEntity mapPoint)
    {
        if (mapPoint.physicalObjectOnMe.Contains(physicalObjectType.player))
        {
            if (mapPoint.immaterialObjectOnMe.Contains(immaterialObjectType.storm))
            {
                if (!GameManager.Instance.player.alreadyMetAStorm)
                {
                    GameManager.Instance.player.alreadyMetAStorm = true;
                    GameEvents.Instance.SecondHeavyRain();
                }
                if (!GameSounds.inAStormPlaying)
                {
                    GameEvents.Instance.EnteringAStorm();
                    GameSounds.inAStormPlaying = true;
                }
            }
            else
            {
                if (GameSounds.inAStormPlaying)
                {
                    GameEvents.Instance.ExitingAStorm();
                    GameSounds.inAStormPlaying = false;
                }
            }
        }
    }

    private Color DisplayRightColor(MapEntity mapPoint)
    {
        #region Display Mountain under my altitude
        underMountainWasDisplayed = false;
        if (mapPoint.immaterialObjectOnMe.Contains(immaterialObjectType.underMountain))
        {
            underMountainWasDisplayed = true;
            tempColor = Colors.underMountain;
        }
        #endregion
        if (mapPoint.physicalObjectOnMe.Count > 0)
        {
            if (mapPoint.physicalObjectOnMe.Contains(physicalObjectType.player))
            {
                if (mapPoint.physicalObjectOnMe.Count > 1)
                    GameEvents.Instance.PlayerIsHit(mapPoint.physicalObjectOnMe);

                if (mapPoint.immaterialObjectOnMe.Contains(immaterialObjectType.wind)) //Je pense vraiment pas qu'il faut faire ça dans l'affichage mais plutôt dans mapObject.
                {
                    GameManager.Instance.player.xPosition += (int)mapPoint.wind.x;
                    GameManager.Instance.player.yPosition += (int)mapPoint.wind.y;
                }
            }
            int indexObjectToDisplay = 1000;
            for (int i = 0; i < mapPoint.physicalObjectOnMe.Count; i++)
            {
                for (int j = 0; j < VisualHierarchy.physicalObjectHierarchy.Length; j++)
                {
                    if (mapPoint.physicalObjectOnMe[i] == VisualHierarchy.physicalObjectHierarchy[j])
                    {
                        indexObjectToDisplay = j < indexObjectToDisplay ? j : indexObjectToDisplay;
                        break;
                    }
                }
            }

            if (indexObjectToDisplay < VisualHierarchy.physicalObjectHierarchy.Length)
            {
                switch (VisualHierarchy.physicalObjectHierarchy[indexObjectToDisplay])
                {
                    case physicalObjectType.mountain:
                        tempColor = Colors.mountain;
                        break;
                    case physicalObjectType.player:
                        tempColor = Colors.player;
                        break;
                    case physicalObjectType.lightning:
                        tempColor = Colors.lightning;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (!underMountainWasDisplayed)
                    tempColor = mapPoint.BaseColor;
            }
        }
        else
        {
            if (!underMountainWasDisplayed)
                tempColor = mapPoint.BaseColor;
        }

        return Overlay(mapPoint, tempColor);
    }

    private Color Overlay(MapEntity mapPoint, Color currentColor)
    {
        if (mapPoint.immaterialObjectOnMe.Count > 0)
        {
            int index = 0;
            for (int i = 0; i < VisualHierarchy.immaterialObjectHierarchy.Length; i++)
            {
                index = VisualHierarchy.immaterialObjectHierarchy.Length - i - 1;
                if (mapPoint.immaterialObjectOnMe.Contains(VisualHierarchy.immaterialObjectHierarchy[index]))
                {
                    switch (VisualHierarchy.immaterialObjectHierarchy[index])
                    {
                        case immaterialObjectType.wind: //animations are visible, not the cloud
                            break;
                        case immaterialObjectType.lightningPrep:
                            currentColor = Color.Lerp(currentColor, Colors.lightningPrep, Colors.lightningPrepAlpha[0]); // selon l'naimation en question
                            break;
                        case immaterialObjectType.storm:
                            currentColor = Color.Lerp(currentColor, Colors.storm, Colors.stormAlpha[0]); //selon le nombre de coor storm autour d'elle
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        if (mapPoint.edgeOfMapDistance < Colors.almostOutOfBounds.Length)
        {
            currentColor = Color.Lerp(currentColor, Colors.outOfBounds, Colors.almostOutOfBounds[mapPoint.edgeOfMapDistance]);

            if (mapPoint.physicalObjectOnMe.Contains(physicalObjectType.player) && !GameManager.Instance.player.alreadyWarnedEdgeOfMap)
            {
                GameManager.Instance.player.alreadyWarnedEdgeOfMap = true;
                GameEvents.Instance.SecondApproachMapEdges();
            }
        }

        return currentColor;
    }

    /*private int NumberOfCellsAroundMeContainingObjectType(MapEntity mapPoint, immaterialObjectType immaterialObjectTypeResearched)
    {
        currentHorizontalIndex
    }*/

    private int DistanceToEdgeOfMap(int xCoordinate, int yCoordinate)
    {
        int closestHorizontalBorder = fullMap.GetLength(0) - 1 - xCoordinate < xCoordinate ? fullMap.GetLength(0) - 1 - xCoordinate : xCoordinate;
        int closestVerticalBorder = fullMap.GetLength(1) - 1 - yCoordinate < yCoordinate ? fullMap.GetLength(1) - 1 - yCoordinate : yCoordinate;

        return closestHorizontalBorder < closestVerticalBorder ? closestHorizontalBorder : closestVerticalBorder;
    }
}
