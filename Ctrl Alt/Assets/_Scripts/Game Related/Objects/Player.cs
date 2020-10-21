using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MapObject me;

    void Awake()
    {
        me = new MapObject(15, 15, objectType.player);

        me.nextRelativePosition = new Vector2(0, 1);
    }

    void OnEnable()
    {
        //GameEvents.Instance.onNextPlayerUpdate += NextAction;
    }

    private void OnDisable()
    {
        //GameEvents.Instance.onNextPlayerUpdate -= NextAction;
    }

    void NextAction()
    {

    }
}
