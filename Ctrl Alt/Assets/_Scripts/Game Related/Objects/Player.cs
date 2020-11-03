using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MapObject
{
    private float selectionTime = 1f;
    private float currentSelectionTime = 0f;
    private Vector2 desiredDirection;


    public Player(int xPositionInit, int yPositionInit, objectType objectType) : base(xPositionInit, yPositionInit, objectType)
    {

    }

    void Awake()
    {
        xPosition = 15;
        yPosition = 15;
        type = objectType.player;


        nextRelativePosition = new Vector2(0, 1);
    }

    void OnEnable()
    {
        GameEvents.Instance.onNextPlayerUpdate += NextAction;
    }

    private void OnDisable()
    {
        GameEvents.Instance.onNextPlayerUpdate -= NextAction;
    }

    void Update()
    {
        if (currentSelectionTime >= selectionTime)
        {
            nextRelativePosition = desiredDirection;
            currentSelectionTime = 0f;
        }

        if (Input.GetButtonDown("Up"))
        {
            if (Input.GetButtonDown("Left"))
            {
                TestIfStillSameInput(new Vector2(-1, 1));
            }
            else if (Input.GetButtonDown("Right"))
            {
                TestIfStillSameInput(new Vector2(1, 1));
            }
            else
            {
                TestIfStillSameInput(new Vector2(0, 1));
            }
        }
        else if (Input.GetButtonDown("Down"))
        {
            if (Input.GetButtonDown("Left"))
            {
                TestIfStillSameInput(new Vector2(-1, -1));
            }
            else if (Input.GetButtonDown("Right"))
            {
                TestIfStillSameInput(new Vector2(1, -1));
            }
            else
            {
                TestIfStillSameInput(new Vector2(0, -1));
            }

        }
        else if (Input.GetButtonDown("Left"))
        {
            TestIfStillSameInput(new Vector2(0, -1));
        }
        else if (Input.GetButtonDown("Right"))
        {
            TestIfStillSameInput(new Vector2(1, 0));
        }
        else
        {
            desiredDirection = Vector2.zero;
            currentSelectionTime = 0f;
        }
    }

    private void TestIfStillSameInput(Vector2 input)
    {
        if (desiredDirection != input)
        {
            currentSelectionTime = 0f;
            desiredDirection = input;
        }
        else
        {
            currentSelectionTime += Time.deltaTime;
        }
    }

    void NextAction()
    {
        xPosition += (int)nextRelativePosition.x;
        yPosition += (int)nextRelativePosition.y;
    }
}
