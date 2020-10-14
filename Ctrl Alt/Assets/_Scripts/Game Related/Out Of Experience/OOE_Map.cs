using System.Collections;
using UnityEngine;

public class OOE_Map : MonoBehaviour
{
    #region Initiatlization
    #region Inhérents au jeu
    private GameObject player;

    
    #endregion

    #region Map Characteristics to become Scriptable Object après les LD elements
    [Header("Map Characteristics")]
    [Tooltip("En case par seconde")]
    [Range(0f, 3f)] public float speed = 1f;
    [Tooltip("La matrice fait 32x32")]
    public int[,] boundariesMap = new int[64, 32];
    #endregion

    #region LD elements to become Scriptable Object
    [Header("LD elements")]
    public Vector2 spawnPlayer;
    public GameObject mountain;
    public GameObject storm;
    public GameObject missile; // ou sinon des éclairs (ronds qui se rapprochent jusqu'à frapper une zone fortement quand ils sont tous sur le même point (la déflagration))
    #endregion
    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        player.transform.position = spawnPlayer;
    }

    void Update()
    {
        
    }
}
