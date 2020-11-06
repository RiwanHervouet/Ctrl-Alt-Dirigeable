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
        if (mapPoint.objectVisualOnMe.Count > 0)
        {
            if (mapPoint.objectVisualOnMe.Count > 1)
            {
                GameEvents.Instance.PlayerIsHit(mapPoint.objectVisualOnMe); 
            }
            int indexObjectToDisplay = 1000;
            for (int i = 0; i < mapPoint.objectVisualOnMe.Count; i++)
            {
                for (int j = 0; j < VisualHierarchy.objectHierarchy.Length; j++)
                {
                    if (mapPoint.objectVisualOnMe[i] == VisualHierarchy.objectHierarchy[j])
                    {
                        indexObjectToDisplay = j < indexObjectToDisplay ? j : indexObjectToDisplay;
                        break;
                    }
                }
            }

            switch (VisualHierarchy.objectHierarchy[indexObjectToDisplay])
            {
                case objectType.mountain:
                    tempColor = Colors.mountain;
                    break;
                case objectType.player:
                    tempColor = Colors.player;
                    break;
                case objectType.lightning:
                    tempColor = Colors.lightning;
                    break;
                default:
                    break;
            }
        }
        else
        {
            tempColor = mapPoint.BaseColor;
        }

        return Overlay(mapPoint, tempColor);
    }

    private Color Overlay(MapEntity mapPoint, Color currentColor)
    {
        if (mapPoint.inAStorm)
        {
            currentColor = Color.Lerp(currentColor, Colors.storm, Colors.stormAlpha);
        }
        /*
        if (mapPoint.windFX)
        {
            currentColor = Color.Lerp(currentColor, Colors.storm, Colors.stormAlpha);
        }
        */
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
