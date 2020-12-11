using System.Collections;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    #region Initialization
    private MapObject stormIBelongTo;
    [Range(0, 20)] public int totalLightningCount = 3;
    [SerializeField] [Range(0, 15)] private int simultaneousLightning = 4;
    [SerializeField] private bool lightningNeighborsPossible = true;
    private MapObject[] allLightningObjects;
    private MapObject[] allLightningPrepObjects;
    private LightningPrep[] allLightningPrepScripts;
    private int lightningsActivatedCount;
    private bool[] lightningActivated;
    private bool[] lightningObjectActivated;
    private bool[] lightningAnnonceActivated;
    #endregion

    void Start()
    {
        stormIBelongTo = stormIBelongTo ? stormIBelongTo : GetComponent<MapObject>();

        for (int i = 0; i < totalLightningCount; i++)
        {
            allLightningObjects[i] = transform.GetChild(i).GetComponent<MapObject>();
            allLightningPrepObjects[i] = allLightningObjects[i].transform.GetChild(0).GetComponent<MapObject>();
            allLightningPrepScripts[i] = allLightningPrepObjects[i].transform.GetComponent<LightningPrep>();
        }

        lightningObjectActivated = new bool[totalLightningCount];
        lightningAnnonceActivated = new bool[totalLightningCount];
        lightningActivated = new bool[totalLightningCount];
        lightningsActivatedCount = 0;

        for (int i = 0; i < allLightningObjects.Length; i++)
        {
            allLightningObjects[i].isActive = false;
            allLightningPrepObjects[i].isActive = false;
        }
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnNextEnvironmentUpdate += LightningManagement;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnNextEnvironmentUpdate -= LightningManagement;
    }

    private void LightningManagement()
    {
        if (lightningsActivatedCount < simultaneousLightning)
        {
            ActivateLightning();
        }
    }

    private void ActivateLightning()
    {
        bool activatedNewLightning = false;
        int lightningIndex = 1000;
        int loopNumber = 0;

        while (!activatedNewLightning && loopNumber < 10)
        {
            loopNumber++;
            if (loopNumber > 8)
            {
                Debug.LogWarning("Can't create new lightnings here");
            }

            lightningIndex = Random.Range(0, totalLightningCount);
            if (!lightningActivated[lightningIndex])
            {
                if (lightningNeighborsPossible)
                {
                    activatedNewLightning = true;
                }
                else
                {
                    if (!lightningActivated[lightningIndex - 1] || !lightningActivated[lightningIndex + 1])
                    {
                        activatedNewLightning = true;
                    }

                }
            }
        }
        CreateNewLightning(lightningIndex);
    }

    private void CreateNewLightning(int index)
    {
        if (index < allLightningPrepScripts.Length)
        {
            allLightningPrepScripts[index].StartAnimation(4);
        }
    }

    void UpdateLightningObjectsData()
    {
        lightningsActivatedCount = 0;
        for (int i = 0; i < totalLightningCount; i++)
        {
            lightningObjectActivated[i] = allLightningObjects[i].isActive;
            lightningAnnonceActivated[i] = allLightningPrepObjects[i].isActive;


            if (lightningObjectActivated[i] || lightningAnnonceActivated[i])
            {
                lightningActivated[i] = true;
            }

            if (lightningActivated[i])
            {
                lightningsActivatedCount++;
            }
        }

        for (int i = 0; i < allLightningObjects.Length; i++)
        {
            if (lightningObjectActivated[i] != allLightningObjects[i].isActive)
            {
                lightningsActivatedCount--;
            }
        }
        if (Random.Range(0, lightningsActivatedCount) < simultaneousLightning)
        {

        }
    }
}
