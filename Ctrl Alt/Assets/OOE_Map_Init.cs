using System.Collections;
using UnityEngine;

public class OOE_Map_Init : MonoBehaviour
{
    #region Initialization
    public MapObjectConstructor[] startObjects;
    public MapObjectConstructor[] cp1Objects;
    #endregion

    void Start()
    {
        for (int i = 0; i < startObjects.Length; i++)
        {
            startObjects[i].Init();
        }
    }
}
