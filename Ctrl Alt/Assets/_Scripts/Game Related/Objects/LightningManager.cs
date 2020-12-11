using System.Collections;
using UnityEngine;

public class LightningManager : MonoBehaviour
{ ////////////////////////////////////////////////////////// tout ce script est du mystère pour l'instant, faut gérer l'activation des éclairs, maintenant taf bien bro :>
    #region Initialization
    private MapObject stormIBelongTo;
    public MapObject[] allLightnings;
    public bool[] lightningsActivated;
    public int lightningsActivatedCount;
    public bool[] lightnings;
    [SerializeField] [Range(0, 15)] private int simultaneousLightning = 4;
    #endregion

    void Awake()
    {
	
    }

    void Start()
    {
        stormIBelongTo = stormIBelongTo ? stormIBelongTo : GetComponent<MapObject>();

        for (int i = 0; i < allLightnings.Length; i++)
        {
            allLightnings[i].isActive = false;

        }
    }

    void Update()
    {
        for (int i = 0; i < allLightnings.Length; i++)
        {
            if (lightningsActivated[i] != allLightnings[i].isActive)
            {
                lightningsActivatedCount--;
            }
        }
        if (Random.Range(0, allLightnings.Length) < simultaneousLightning) 
        {

        }
    }
}
