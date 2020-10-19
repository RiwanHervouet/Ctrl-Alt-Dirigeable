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
    private Image[,] fullmap;
    public Vector2 matrixTopLeftCoordinate;
    public Vector2 previousNextRelativePostion;
    #endregion

    void Awake()
    {
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
        GameEvents.Instance.onNextEnvironmentUpdate += UpdateMatrix;
        InitMatrix();
    }

    void OnDestroy()
    {
        GameEvents.Instance.onNextEnvironmentUpdate -= UpdateMatrix;
    }

    void UpdateMatrix()
    {
        //ooe_Map.fullMap
    }

    void InitMatrix()
    {
        matrixTopLeftCoordinate = new Vector2(ooe_Map.playerMO.xPosition - 15, ooe_Map.playerMO.yPosition - 15);
    }

    void UpdateTopLeftMatrix()
    {
        matrixTopLeftCoordinate += ooe_Map.playerMO.nextRelativePosition;

        previousNextRelativePostion = ooe_Map.playerMO.nextRelativePosition; //sert quand j'adapte la caméra du à vers où se dirige le joueur
    }
}
