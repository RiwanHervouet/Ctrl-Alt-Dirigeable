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
        for (int j = 0; j < fullMap.GetLength(1); j++)
        {
            for (int i = 0; i < fullMap.GetLength(0); i++)
            {
                fullMap[i, j].myColor = DisplayRightColor(fullMap[i, j]);
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
            if (mapPoint.physicalObjectOnMe.Count > 1)
            {
                if(mapPoint.physicalObjectOnMe.Contains(physicalObjectType.player))
                {
                    GameEvents.Instance.PlayerIsHit(mapPoint.physicalObjectOnMe);
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
            if(!underMountainWasDisplayed)
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
                        case immaterialObjectType.wind:
                            currentColor = Color.Lerp(currentColor, Colors.wind, Colors.windAlpha);
                            break;
                        case immaterialObjectType.lightningPrep:
                            currentColor = Color.Lerp(currentColor, Colors.lightningPrep, Colors.lightningPrepAlpha);
                            break;
                        case immaterialObjectType.storm:
                            currentColor = Color.Lerp(currentColor, Colors.storm, Colors.stormAlpha);
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
        }

        return currentColor;
    }

    private int DistanceToEdgeOfMap(int xCoordinate, int yCoordinate)
    {
        int closestHorizontalBorder = fullMap.GetLength(0) - 1 - xCoordinate < xCoordinate ? fullMap.GetLength(0) - 1 - xCoordinate : xCoordinate;
        int closestVerticalBorder = fullMap.GetLength(1) - 1 - yCoordinate < yCoordinate ? fullMap.GetLength(1) - 1 - yCoordinate : yCoordinate;

        return closestHorizontalBorder < closestVerticalBorder ? closestHorizontalBorder : closestVerticalBorder;
    }
}
