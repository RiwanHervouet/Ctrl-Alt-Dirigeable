using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private float selectionTime = 1f;
    private float currentSelectionTime = 0f;
    private Vector2 desiredDirection;


    void Update()
    {
        if (currentSelectionTime >= selectionTime)
        {
            //GameEvents.Instance.onPlayerInput......
            //nextRelativePosition = desiredDirection;
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
}
