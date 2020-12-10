using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inputs : MonoBehaviour
{
    public enum inputs { UP, DOWN, LEFT, RIGHT, UP_RIGHT, UP_LEFT, DOWN_RIGHT, DOWN_LEFT, ESCAPE, BOTTOMALTITUDE, MIDDLEALTITUDE, TOPALTITUDE, NULL };
    public int inputsUsed;

    private List<inputs> validatedInput = new List<inputs>();
    private inputs movementInput;
    private float currentSelectionTime = 0f;
    private inputs alternativeInput;
    private float currentParallelSelectionTime = 0f;
    private Vector2 lastMovementInput = Vector2.zero;
    private bool pause = false;
    private bool altitudeChange = false;


    void Update()
    {
        CleanValidatedInputList();

        if (GameManager.Instance.canReceiveInput)
        {
            if (Input.GetButton("Up"))
            {
                if (Input.GetButton("Left"))
                {
                    TestIfStillSameInput(inputs.UP_LEFT, true);
                }
                else if (Input.GetButton("Right"))
                {
                    TestIfStillSameInput(inputs.UP_RIGHT, true);
                }
                else
                {
                    TestIfStillSameInput(inputs.UP, true);
                }
            }
            else if (Input.GetButton("Down"))
            {
                if (Input.GetButton("Left"))
                {
                    TestIfStillSameInput(inputs.DOWN_LEFT, true);
                }
                else if (Input.GetButton("Right"))
                {
                    TestIfStillSameInput(inputs.DOWN_RIGHT, true);
                }
                else
                {
                    TestIfStillSameInput(inputs.DOWN, true);
                }
            }
            else if (Input.GetButton("Left"))
            {
                TestIfStillSameInput(inputs.LEFT, true);
            }
            else if (Input.GetButton("Right"))
            {
                TestIfStillSameInput(inputs.RIGHT, true);
            }
            else
            {
                movementInput = inputs.NULL;
                currentSelectionTime = 0f;
            }

            if (Input.GetButton("Escape"))
            {
                TestIfStillSameInput(inputs.ESCAPE, false);
            }
            else if (Input.GetButton("Altitude Bottom"))
            {
                TestIfStillSameInput(inputs.BOTTOMALTITUDE, false);
            }
            else if (Input.GetButton("Altitude Middle"))
            {
                TestIfStillSameInput(inputs.MIDDLEALTITUDE, false);
            }
            else if (Input.GetButton("Altitude Top"))
            {
                TestIfStillSameInput(inputs.TOPALTITUDE, false);
            }
            else
            {
                alternativeInput = inputs.NULL;
                currentParallelSelectionTime = 0f;
            }


            GameEvents.Instance.MapInputCompletion(currentSelectionTime > currentParallelSelectionTime ? currentSelectionTime / GameManager.Instance.inputSelectionTime : currentParallelSelectionTime / GameManager.Instance.inputSelectionTime, currentSelectionTime > currentParallelSelectionTime ? movementInput : alternativeInput);

            if (currentSelectionTime >= GameManager.Instance.inputSelectionTime || currentParallelSelectionTime >= GameManager.Instance.inputSelectionTime)
            {
                if (!GameTime.Instance.gameIsPaused)
                {
                    if (currentSelectionTime > currentParallelSelectionTime)
                    {
                        validatedInput.Add(movementInput);
                        if (ValidatedInput() != lastMovementInput)
                        {
                            Debug.Log("input changed : " + ValidatedInput());
                            GameEvents.Instance.OnPlayerDirectionChange += ValidatedInput;
                            GameEvents.Instance.MapInputCompleted();
                            lastMovementInput = ValidatedInput();
                        }
                        else
                        {
                            validatedInput.Remove(movementInput);
                        }
                    }
                    else
                    {
                        validatedInput.Add(alternativeInput);

                        if (FirstAlternativeInput() == inputs.NULL)
                        {
                            validatedInput.Remove(FirstAlternativeInput());
                        }
                        else
                        {
                            if (FirstAlternativeInput() == inputs.MIDDLEALTITUDE)
                            {
                                Debug.Log("GameManager changé vers middle");
                                GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.MiddleAltitude;
                            }
                            else if (FirstAlternativeInput() == inputs.TOPALTITUDE)
                            {
                                Debug.Log("GameManager changé vers top");
                                GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.TopAltitude;
                            }
                            else
                            {
                                Debug.Log("GameManager changé vers bottom");
                                GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.BottomAltitude;
                            }
                            validatedInput.Remove(FirstAlternativeInput());
                        }


                        GameEvents.Instance.MapInputCompleted();
                    }
                }
                if (pause)
                {
                    GameTime.Instance.gameIsPaused = GameTime.Instance.gameIsPaused ? false : true;
                    pause = false;
                }
                currentSelectionTime = 0f;
                currentParallelSelectionTime = 0f;
            }
        }
    }


    private void TestIfStillSameInput(inputs input, bool isMovementNotParallel)
    {
        if (isMovementNotParallel)
        {
            if (movementInput != input)
            {
                currentSelectionTime = 0f;
                movementInput = input;
            }
            else
            {
                currentSelectionTime += Time.deltaTime;
            }
        }
        else
        {
            if (alternativeInput != input)
            {
                currentParallelSelectionTime = 0f;
                alternativeInput = input;
            }
            else
            {
                currentParallelSelectionTime += Time.deltaTime;
            }
        }
    }

    private inputs FirstAlternativeInput()
    {
        for (int i = 0; i < validatedInput.Count; i++)
        {
            if (!validatedInput[i].ToString().Contains("DOWN") ||
                !validatedInput[i].ToString().Contains("UP") ||
                !validatedInput[i].ToString().Contains("LEFT") ||
                !validatedInput[i].ToString().Contains("RIGHT") ||
                !validatedInput[i].ToString().Contains("NULL"))
            {
                return validatedInput[i];
            }
        }
        return inputs.NULL;
    }
    private Vector2 ValidatedInput()
    {
        switch (validatedInput[0])
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
            case inputs.BOTTOMALTITUDE:
                Debug.Log("input bottom reconnu");
                altitudeChange = true;
                return Vector2.zero;
            case inputs.MIDDLEALTITUDE:
                Debug.Log("input middle reconnu");
                altitudeChange = true;
                return Vector2.zero;
            case inputs.TOPALTITUDE:
                Debug.Log("input top reconnu");
                altitudeChange = true;
                return Vector2.zero;
            default:
                return Vector2.zero;
        }
    }
    private void CleanValidatedInputList()
    {
        for (int i = 0; i < inputsUsed; i++)
        {
            validatedInput.RemoveAt(0);
        }
        inputsUsed = 0;
    }
}
