using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Initiatlization
    [Range(1,6)]public int speed = 3;
    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        GameEvents.Instance.onNextTurn += NextAction;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.onNextTurn -= NextAction;
    }

    void NextAction()
    {

    }
}
