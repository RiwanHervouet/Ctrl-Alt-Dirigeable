using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OOE_Map : MonoBehaviour
{
    #region Initiatlization

    #region Inhérents au jeu
    public GameObject player;
    public MapObject playerMO;
    public Vector2 matrixTopLeftCoordinate;
    #endregion

    #region Map Characteristics to become Scriptable Object après les LD elements
    [Header("Map Characteristics")]
    [Tooltip("La matrice fait 32x32")]
    public MapEntity[,] fullMap = new MapEntity[64, 32];
    #endregion

    #region LD elements to become Scriptable Object
    [Header("LD elements")]
    public Vector2 spawnPlayer;
    public GameObject mountain;
    public GameObject storm;
    public GameObject missile; // ou sinon des éclairs (ronds qui se rapprochent jusqu'à frapper une zone fortement quand ils sont tous sur le même point (la déflagration))

    public Color backgroundColor;
    public Color playerColor;
    public Color mountainColor;
    #endregion

    #endregion

    void Start()
    {
        player.transform.position = spawnPlayer;
        GameEvents.Instance.onNextTurn += NextTurn;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onNextTurn -= NextTurn;
    }

    private void NextTurn()
    {
        //player
        //things happen
    }
}
