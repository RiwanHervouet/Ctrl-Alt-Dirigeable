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
public class Module_Map : MonoBehaviour
{
    #region Initiatlization
    private Image[,] matrix = new Image[32, 32];

    private Transform matrixParent;
    private Transform tempParent;

    private OOE_Map ooe_Map;
    public Vector2 matrixTopLeftCoordinate;
    public Vector2 previousNextRelativePostion;
    #endregion

    void Awake()
    {
        matrixParent = gameObject.GetComponent<Transform>();

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
        GameEvents.Instance.onNextRefresh += UpdateMatrix;
        InitMatrix();
        GameEvents.Instance.onNextPlayerUpdate += UpdateTopLeftMatrix;
    }

    void OnDestroy()
    {
        GameEvents.Instance.onNextRefresh -= UpdateMatrix;
        GameEvents.Instance.onNextPlayerUpdate -= UpdateTopLeftMatrix;
    }

    void UpdateMatrix()
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, j].color = ooe_Map.fullMap[i + (int)matrixTopLeftCoordinate.x, j + (int)matrixTopLeftCoordinate.y].myColor;
            }
        }
    }

    void InitMatrix()
    {
        matrixTopLeftCoordinate = new Vector2(ooe_Map.playerMO.me.xPosition - 15, ooe_Map.playerMO.me.yPosition - 15);
    }

    void UpdateTopLeftMatrix()
    {
        matrixTopLeftCoordinate += ooe_Map.playerMO.me.nextRelativePosition;

        previousNextRelativePostion = ooe_Map.playerMO.me.nextRelativePosition; //sert quand j'adapte la caméra du à vers où se dirige le joueur
    }
}
