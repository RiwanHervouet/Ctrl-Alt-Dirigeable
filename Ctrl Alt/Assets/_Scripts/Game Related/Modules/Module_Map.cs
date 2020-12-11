using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Module_Map : MonoBehaviour
{
    #region Initiatlization
    private Image[,] matrix = new Image[32, 32];

    private Transform matrixParent;
    private Transform tempParent;
    private float fadeOutStartTime;
    private Color tempColor;

    public MapObject mo_Player;
    public Vector2 matrixTopLeftCoordinate;
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
        mo_Player = mo_Player ? mo_Player : GameManager.Instance.player;
        GameEvents.Instance.OnNextRefresh += UpdateMatrix;
        GameEvents.Instance.OnNextPlayerUpdate += UpdateTopLeftMatrix;
        GameEvents.Instance.OnMapInputCompletion += DrawInputCompletion;
        GameEvents.Instance.OnMapInputCompleted += InputCompletedFadeOut;
    }

    void OnDestroy()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnNextRefresh -= UpdateMatrix;
            GameEvents.Instance.OnNextPlayerUpdate -= UpdateTopLeftMatrix;
            GameEvents.Instance.OnMapInputCompletion -= DrawInputCompletion;
            GameEvents.Instance.OnMapInputCompleted -= InputCompletedFadeOut;
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
                    matrix[i, j].color = GameManager.Instance.mapScript.fullMap[i + (int)matrixTopLeftCoordinate.x, j + (int)matrixTopLeftCoordinate.y].myColor;
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
    }

    private Vector2[] clockwiseOuterMatrixPoints =
        {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(2, 0),
        new Vector2(3, 0),
        new Vector2(4, 0),
        new Vector2(5, 0),
        new Vector2(6, 0),
        new Vector2(7, 0),
        new Vector2(8, 0),
        new Vector2(9, 0),
        new Vector2(10, 0),
        new Vector2(11, 0),
        new Vector2(12, 0),
        new Vector2(13, 0),
        new Vector2(14, 0),
        new Vector2(15, 0),
        new Vector2(16, 0),
        new Vector2(17, 0),
        new Vector2(18, 0),
        new Vector2(19, 0),
        new Vector2(20, 0),
        new Vector2(21, 0),
        new Vector2(22, 0),
        new Vector2(23, 0),
        new Vector2(24, 0),
        new Vector2(25, 0),
        new Vector2(26, 0),
        new Vector2(27, 0),
        new Vector2(28, 0),
        new Vector2(29, 0),
        new Vector2(30, 0),
        new Vector2(31, 0),
        new Vector2(31, 1),
        new Vector2(31, 2),
        new Vector2(31, 3),
        new Vector2(31, 4),
        new Vector2(31, 5),
        new Vector2(31, 6),
        new Vector2(31, 7),
        new Vector2(31, 8),
        new Vector2(31, 9),
        new Vector2(31, 10),
        new Vector2(31, 11),
        new Vector2(31, 12),
        new Vector2(31, 13),
        new Vector2(31, 14),
        new Vector2(31, 15),
        new Vector2(31, 16),
        new Vector2(31, 17),
        new Vector2(31, 18),
        new Vector2(31, 19),
        new Vector2(31, 20),
        new Vector2(31, 21),
        new Vector2(31, 22),
        new Vector2(31, 23),
        new Vector2(31, 24),
        new Vector2(31, 25),
        new Vector2(31, 26),
        new Vector2(31, 27),
        new Vector2(31, 28),
        new Vector2(31, 29),
        new Vector2(31, 30),
        new Vector2(31, 31),
        new Vector2(30, 31),
        new Vector2(29, 31),
        new Vector2(28, 31),
        new Vector2(27, 31),
        new Vector2(26, 31),
        new Vector2(25, 31),
        new Vector2(24, 31),
        new Vector2(23, 31),
        new Vector2(22, 31),
        new Vector2(21, 31),
        new Vector2(20, 31),
        new Vector2(19, 31),
        new Vector2(18, 31),
        new Vector2(17, 31),
        new Vector2(16, 31),
        new Vector2(15, 31),
        new Vector2(14, 31),
        new Vector2(13, 31),
        new Vector2(12, 31),
        new Vector2(11, 31),
        new Vector2(10, 31),
        new Vector2(9, 31),
        new Vector2(8, 31),
        new Vector2(7, 31),
        new Vector2(6, 31),
        new Vector2(5, 31),
        new Vector2(4, 31),
        new Vector2(3, 31),
        new Vector2(2, 31),
        new Vector2(1, 31),
        new Vector2(0, 31),
        new Vector2(0, 30),
        new Vector2(0, 29),
        new Vector2(0, 28),
        new Vector2(0, 27),
        new Vector2(0, 26),
        new Vector2(0, 25),
        new Vector2(0, 24),
        new Vector2(0, 23),
        new Vector2(0, 22),
        new Vector2(0, 21),
        new Vector2(0, 20),
        new Vector2(0, 19),
        new Vector2(0, 18),
        new Vector2(0, 17),
        new Vector2(0, 16),
        new Vector2(0, 15),
        new Vector2(0, 14),
        new Vector2(0, 13),
        new Vector2(0, 12),
        new Vector2(0, 11),
        new Vector2(0, 10),
        new Vector2(0, 9),
        new Vector2(0, 8),
        new Vector2(0, 7),
        new Vector2(0, 6),
        new Vector2(0, 5),
        new Vector2(0, 4),
        new Vector2(0, 3),
        new Vector2(0, 2),
        new Vector2(0, 1)
        };
    private int outerPointsIndexStart = 0;
    private int outerPointsIndexClockwise= 0;
    private int outerPointsIndexCounterClockwise= 0;
    void DrawInputCompletion(float inputCompletion, Inputs.inputs input)
    {
        switch (input)
        {
            case Inputs.inputs.UP_LEFT:
                outerPointsIndexStart = 0;
                break;
            case Inputs.inputs.UP:
                outerPointsIndexStart = 15;
                break;
            case Inputs.inputs.UP_RIGHT:
                outerPointsIndexStart = 31;
                break;
            case Inputs.inputs.RIGHT:
                outerPointsIndexStart = 46;
                break;
            case Inputs.inputs.DOWN_RIGHT:
                outerPointsIndexStart = 62;
                break;
            case Inputs.inputs.DOWN:
                outerPointsIndexStart = 78;
                break;
            case Inputs.inputs.DOWN_LEFT:
                outerPointsIndexStart = 93;
                break;
            case Inputs.inputs.LEFT:
                outerPointsIndexStart = 109;
                break;
            case Inputs.inputs.ALTITUDE_HIGHER:
                outerPointsIndexStart = 120;
                break;
            case Inputs.inputs.ALTITUDE_LOWER:
                outerPointsIndexStart = 98;
                break;
            case Inputs.inputs.SPEED_SLOWER:
                outerPointsIndexStart = 7;
                break;
            case Inputs.inputs.SPEED_FASTER:
                outerPointsIndexStart = 23;
                break;
            default:
                inputCompletion = 0;
                break;
        }

        int numberOfLEDsLit = Mathf.CeilToInt(clockwiseOuterMatrixPoints.Length * inputCompletion);
        for (int i = 0; i < Mathf.FloorToInt(numberOfLEDsLit * 0.5f); i++)
        {
            outerPointsIndexCounterClockwise = outerPointsIndexStart - i < 0 ?
                outerPointsIndexStart - i + clockwiseOuterMatrixPoints.Length :
                outerPointsIndexStart - i;
            outerPointsIndexClockwise = outerPointsIndexStart + i > clockwiseOuterMatrixPoints.Length - 1 ?
                outerPointsIndexStart + i - clockwiseOuterMatrixPoints.Length :
                outerPointsIndexStart + i;

            matrix[
                (int)clockwiseOuterMatrixPoints[outerPointsIndexCounterClockwise].x, 
                (int)clockwiseOuterMatrixPoints[outerPointsIndexCounterClockwise].y
                ].color = Colors.directionInput;
            matrix[
                (int)clockwiseOuterMatrixPoints[outerPointsIndexClockwise].x, 
                (int)clockwiseOuterMatrixPoints[outerPointsIndexClockwise].y
                ].color = Colors.directionInput;
        }
    }

    void InputCompletedFadeOut()
    {
        fadeOutStartTime = Time.time;
        GameManager.Instance.canReceiveInput = false;
        StartCoroutine(CompleteFadeOut());
    }

    IEnumerator CompleteFadeOut()
    {
        while ((Time.time - fadeOutStartTime) / Colors.completedInputFadeOutTime < 1)
        {
            for (int i = 0; i < clockwiseOuterMatrixPoints.Length; i++)
            {
                if (CoordinateIsWithinTheMap(
                        (int)clockwiseOuterMatrixPoints[i].x + (int)matrixTopLeftCoordinate.x,
                        (int)clockwiseOuterMatrixPoints[i].y + (int)matrixTopLeftCoordinate.y))
                {
                    tempColor = GameManager.Instance.mapScript.fullMap[
                            (int)clockwiseOuterMatrixPoints[i].x + (int)matrixTopLeftCoordinate.x,
                            (int)clockwiseOuterMatrixPoints[i].y + (int)matrixTopLeftCoordinate.y
                            ].myColor;
                }
                else
                {
                    tempColor = Colors.outOfBounds;
                }

                matrix[(int)clockwiseOuterMatrixPoints[i].x, (int)clockwiseOuterMatrixPoints[i].y].color = Color.Lerp(
                   Colors.directionInput,
                   tempColor,
                   (Time.time - fadeOutStartTime) / Colors.completedInputFadeOutTime);
            }
            yield return new WaitForFixedUpdate();
        }

        GameManager.Instance.canReceiveInput = true;
        StopCoroutine(CompleteFadeOut());
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
