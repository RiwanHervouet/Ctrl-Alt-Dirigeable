using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public enum inputs { UP, DOWN, LEFT, RIGHT, UP_RIGHT, UP_LEFT, DOWN_RIGHT, DOWN_LEFT, ESCAPE, ALTITUDE_LOWER, ALTITUDE_HIGHER, SPEED_SLOWER, SPEED_FASTER, NULL };

    [HideInInspector] public List<inputs> ctrlAltInput = new List<inputs>();
    private bool gazeMovementInput = false;
    private bool gazeAlternativeInput = false;
    private List<inputs> validatedInput = new List<inputs>();
    private inputs movementInput;
    private float currentSelectionTime = 0f;
    private inputs alternativeInput;
    private float currentParallelSelectionTime = 0f;
    private Vector2 lastMovementInput = Vector2.zero;
    private bool pause = false;


    void OnEnable()
    {
        GameEvents.Instance.OnCtrlAltInputSent += UpdateInputList;
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCtrlAltInputSent -= UpdateInputList;
        }
    }
    void Update()
    {
        CleanValidatedInputList();


        if (!GameManager.Instance.canReceiveInput)
        {
            if (Input.GetButton("Escape"))
            {
                currentParallelSelectionTime += Time.deltaTime;
                if (currentParallelSelectionTime > GameManager.Instance.inputSelectionTime)
                {
                    GameTime.Instance.gameIsPaused = false;
                }
            }
            else
            {
                currentParallelSelectionTime = 0f;
            }
        }
        else
        {
            CtrlAltInputRegistration();

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
                if (!gazeMovementInput)
                {
                    movementInput = inputs.NULL;
                    currentSelectionTime = 0f;
                }
            }

            if (Input.GetButton("Escape"))
            {
                TestIfStillSameInput(inputs.ESCAPE, false);
            }
            else if (Input.GetButton("Altitude Lower"))
            {
                TestIfStillSameInput(inputs.ALTITUDE_LOWER, false);
            }
            else if (Input.GetButton("Altitude Higher"))
            {
                TestIfStillSameInput(inputs.ALTITUDE_HIGHER, false);
            }
            else if (Input.GetButton("Speed Slower"))
            {
                TestIfStillSameInput(inputs.SPEED_SLOWER, false);
            }
            else if (Input.GetButton("Speed Faster"))
            {
                TestIfStillSameInput(inputs.SPEED_FASTER, false);
            }
            else
            {
                if (!gazeAlternativeInput)
                {
                    alternativeInput = inputs.NULL;
                    currentParallelSelectionTime = 0f;
                }
            }


            GameEvents.Instance.MapInputCompletion(currentSelectionTime > currentParallelSelectionTime ? currentSelectionTime / GameManager.Instance.inputSelectionTime : currentParallelSelectionTime / GameManager.Instance.inputSelectionTime, currentSelectionTime > currentParallelSelectionTime ? movementInput : alternativeInput);

            if (currentSelectionTime >= GameManager.Instance.inputSelectionTime || currentParallelSelectionTime >= GameManager.Instance.inputSelectionTime)
            {
                if (!GameTime.Instance.gameIsPaused)
                {
                    if (currentSelectionTime > currentParallelSelectionTime)
                    {
                        validatedInput.Add(movementInput);
                        if (ValidatedInput() != lastMovementInput && ValidatedInput() != Vector2.zero)
                        {
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
                            if (FirstAlternativeInput() == inputs.ALTITUDE_LOWER)
                            {
                                if (GameManager.Instance.player.nextAltitudeGoal == GameManager.altitudes.MiddleAltitude)
                                {
                                    GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.BottomAltitude;
                                }
                                if (GameManager.Instance.player.nextAltitudeGoal == GameManager.altitudes.TopAltitude)
                                {
                                    GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.MiddleAltitude;
                                }
                            }
                            else if (FirstAlternativeInput() == inputs.ALTITUDE_HIGHER)
                            {
                                if (GameManager.Instance.player.nextAltitudeGoal == GameManager.altitudes.MiddleAltitude)
                                {
                                    GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.TopAltitude;
                                }
                                if (GameManager.Instance.player.nextAltitudeGoal == GameManager.altitudes.BottomAltitude)
                                {
                                    GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.MiddleAltitude;
                                }
                            }
                            else if (FirstAlternativeInput() == inputs.SPEED_SLOWER)
                            {
                                GameTime.Instance.ChangePlayerSpeed(inputs.SPEED_SLOWER);
                            }
                            else if (FirstAlternativeInput() == inputs.SPEED_FASTER)
                            {
                                GameTime.Instance.ChangePlayerSpeed(inputs.SPEED_FASTER);
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
        if (validatedInput.Count <= 0)
            return Vector2.zero;

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
            case inputs.ALTITUDE_LOWER:
                return Vector2.zero;
            case inputs.ALTITUDE_HIGHER:
                return Vector2.zero;
            default:
                return Vector2.zero;
        }
    }

    private void CleanValidatedInputList()
    {
        for (int i = 0; i < GameManager.Instance.cleanInputList; i++)
        {
            if (validatedInput.Count != 0)
            {
                validatedInput.RemoveAt(0);
            }
        }
        GameManager.Instance.cleanInputList = 0;
    }

    private void CtrlAltInputRegistration()
    {
        gazeMovementInput = false;
        gazeAlternativeInput = false;
        for (int i = 0; i < ctrlAltInput.Count; i++)
        {
            TestIfStillSameInput(ctrlAltInput[i], (int)ctrlAltInput[i] <= 7 ? true : false);
            if ((int)ctrlAltInput[i] <= 7)
            {
                gazeMovementInput = true;
            }
            else
            {
                if (ctrlAltInput[i] != inputs.NULL)
                    gazeAlternativeInput = true;
            }
        }

        ctrlAltInput.Clear();
    }

    private void UpdateInputList(inputs inputReceived, bool isActive)
    {
        if (isActive)
            ctrlAltInput.Add(inputReceived);
    }
}
