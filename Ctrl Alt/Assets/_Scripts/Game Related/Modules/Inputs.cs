using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inputs : MonoBehaviour
{
    public enum inputs { UP, DOWN, LEFT, RIGHT, UP_RIGHT, UP_LEFT, DOWN_RIGHT, DOWN_LEFT, ESCAPE, NULL };

    private float currentSelectionTime = 0f;
    private inputs desiredInput;
    private Vector2 lastInput = Vector2.zero;
    private bool pause = false;


    void Update()
    {
        if (GameManager.Instance.canReceiveInput)
        {
            if (Input.GetButton("Up"))
            {
                if (Input.GetButton("Left"))
                {
                    TestIfStillSameInput(inputs.UP_LEFT);
                }
                else if (Input.GetButton("Right"))
                {
                    TestIfStillSameInput(inputs.UP_RIGHT);
                }
                else
                {
                    TestIfStillSameInput(inputs.UP);
                }
            }
            else if (Input.GetButton("Down"))
            {
                if (Input.GetButton("Left"))
                {
                    TestIfStillSameInput(inputs.DOWN_LEFT);
                }
                else if (Input.GetButton("Right"))
                {
                    TestIfStillSameInput(inputs.DOWN_RIGHT);
                }
                else
                {
                    TestIfStillSameInput(inputs.DOWN);
                }
            }
            else if (Input.GetButton("Left"))
            {
                TestIfStillSameInput(inputs.LEFT);
            }
            else if (Input.GetButton("Right"))
            {
                TestIfStillSameInput(inputs.RIGHT);
            }
            else
            {
                desiredInput = inputs.NULL;
                currentSelectionTime = 0f;
            }

            if (Input.GetButton("Escape"))
            {
                TestIfStillSameInput(inputs.ESCAPE);
            }


            GameEvents.Instance.MapInputCompletion(currentSelectionTime / GameManager.Instance.inputSelectionTime, desiredInput);
            if (currentSelectionTime >= GameManager.Instance.inputSelectionTime)
            {
                if (!GameTime.Instance.gameIsPaused)
                {
                    if (CurrentInput() != lastInput)
                    {
                        Debug.Log("input changed : " + CurrentInput());
                        GameEvents.Instance.OnPlayerDirectionChange += CurrentInput;
                        GameEvents.Instance.MapInputCompleted();
                        lastInput = CurrentInput();
                    }
                }
                if (pause)
                {
                    GameTime.Instance.gameIsPaused = GameTime.Instance.gameIsPaused ? false : true;
                    pause = false;
                }
                currentSelectionTime = 0f;
            }
        }
    }

    private void TestIfStillSameInput(inputs input)
    {
        if (desiredInput != input)
        {
            currentSelectionTime = 0f;
            desiredInput = input;
        }
        else
        {
            currentSelectionTime += Time.deltaTime;
        }
    }

    private Vector2 CurrentInput()
    {
        switch (desiredInput)
        {
            case inputs.UP:
                return new Vector2(0, -1);
            case inputs.DOWN:
                return new Vector2(0, 1);
            case inputs.LEFT:
                return new Vector2(-1, 0);
            case inputs.RIGHT:
                return new Vector2(1, 0);
            case inputs.UP_RIGHT:
                return new Vector2(1, -1);
            case inputs.UP_LEFT:
                return new Vector2(-1, -1);
            case inputs.DOWN_RIGHT:
                return new Vector2(1, 1);
            case inputs.DOWN_LEFT:
                return new Vector2(-1, 1);
            case inputs.ESCAPE:
                pause = true;
                return Vector2.zero;
            default:
                return Vector2.zero;
        }
    }
}
