using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//MODULES ont les inputs et outputs alors que OutOfExperience sont le système

/* 1. Représenter de n'importe quelle manière un objet sur la carte
 * 2. Représenter d'autres
 * 3. L'afficher sur la carte
 *      aka changer la couleur des LEDs 
 *      aka avoir l'ordre de priorité
*/
[SelectionBase]
public class Module_Map : MonoBehaviour
{
    #region Initiatlization
    private Image[,] matrix = new Image[32, 32];

    private Transform matrixParent;
    private Transform tempParent;

    public OOE_Map ooe_Map;
    public MapObject mo_Player;
    public Vector2 matrixTopLeftCoordinate;
    #endregion

    void Awake()
    {
        matrixParent = gameObject.GetComponent<Transform>();

        ooe_Map = transform.parent.GetChild(2).GetComponent<OOE_Map>();

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            tempParent = matrixParent.GetChild(j).transform;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, j] = tempParent.GetChild(i).GetComponent<Image>(); //dégage si scriptableobject
            }
        }
    }

    void Start()
    {
        GameEvents.Instance.OnNextRefresh += UpdateMatrix;
        GameEvents.Instance.OnNextPlayerUpdate += UpdateTopLeftMatrix;
    }

    void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnNextRefresh -= UpdateMatrix;
            GameEvents.Instance.OnNextPlayerUpdate -= UpdateTopLeftMatrix;
        }
    }

    void UpdateMatrix()
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (CoordinateIsWithinTheMap(i + (int)matrixTopLeftCoordinate.x, j + (int)matrixTopLeftCoordinate.y))
                {
                    matrix[i, j].color = ooe_Map.fullMap[i + (int)matrixTopLeftCoordinate.x, j + (int)matrixTopLeftCoordinate.y].myColor;
                }
                else
                {
                    matrix[i, j].color = Colors.outOfBounds;
                }
            }
        }
    }

    void UpdateTopLeftMatrix()
    {
        matrixTopLeftCoordinate = new Vector2(mo_Player.xPosition - 15, mo_Player.yPosition - 16);

        //previousNextRelativePostion = mo_Player.nextRelativePosition; //sert quand j'adapte la caméra du à vers où se dirige le joueur
    }

    bool CoordinateIsWithinTheMap(int xCoordinate, int yCoordinate)
    {
        if (xCoordinate > 0)

            if (xCoordinate < ooe_Map.fullMap.GetLength(0))

                if (yCoordinate > 0)

                    if (yCoordinate < ooe_Map.fullMap.GetLength(1))

                        return true;

        return false;
    }
}
