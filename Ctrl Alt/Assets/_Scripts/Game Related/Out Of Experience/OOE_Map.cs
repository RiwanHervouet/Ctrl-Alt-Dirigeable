using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public MapEntity[,] fullMap = new MapEntity[64, 32];
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

    #endregion

    void Start()
    {
        //player.transform.position = spawnPlayer;

        //playerMO = player.GetComponent<Player>();

        for (int j = 0; j < fullMap.GetLength(1); j++)
        {
            tempParent = transform.GetChild(j).transform;
            for (int i = 0; i < fullMap.GetLength(0); i++)
            {
                fullMap[i, j] = new MapEntity(
                        tempParent.GetChild(i).GetComponent<Image>(),
                        Mathf.FloorToInt(i * 0.125f - j * .015625f) % 2 == 0 ? Colors.background1 : Colors.background2
                        );
            }
        }
    }
    void OnEnable()
    {
        GameEvents.Instance.onNextPlayerUpdate += NextPlayerUpdate;
        GameEvents.Instance.onNextEnvironmentUpdate += NextEnvironmentUpdate;
        GameEvents.Instance.onNextRefresh += NextRefresh;
    }
    private void OnDisable()
    {
        GameEvents.Instance.onNextPlayerUpdate -= NextPlayerUpdate;
        GameEvents.Instance.onNextEnvironmentUpdate -= NextEnvironmentUpdate;
        GameEvents.Instance.onNextRefresh -= NextRefresh;
    }

    private void NextPlayerUpdate()
    {
        //player
        //things happen
    }

    private void NextEnvironmentUpdate()
    {
        //things happen
    }

    private void NextRefresh()
    {
        for (int j = 0; j < fullMap.GetLength(1); j++)
        {
            for (int i = 0; i < fullMap.GetLength(0); i++)
            {
                fullMap[i, j].myColor = fullMap[i, j].BaseColor; //MethodPourTrouverLaCouleurQuiCorrespondEnFonctionDesObjetsAuDessus();
            }
        }
    }
}
