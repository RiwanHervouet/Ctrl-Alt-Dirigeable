using System.Collections;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class EyeTrackerInput : MonoBehaviour
{
    #region Initialization
    public GazeAware gazeComponent;
    public Inputs.inputs myInput
    {
        get
        {
            return _myInput;
        }
    }
    [SerializeField] private Inputs.inputs _myInput;

    public bool souffleurAffected = false;
    public Inputs.inputs mySecondInput
    {
        get
        {
            return souffleurInhaledInput;
        }
    }
    [SerializeField] private Inputs.inputs souffleurInhaledInput;
    private bool isInhaled = false;
    private bool isExhaled = false;
    #endregion

    private void OnEnable()
    {
        GameEvents.Instance.OnSouffleurInhaled += IsSouffleurInhaled;
        GameEvents.Instance.OnSouffleurExhaled += IsSouffleurExhaled;
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnSouffleurInhaled -= IsSouffleurInhaled;
            GameEvents.Instance.OnSouffleurExhaled -= IsSouffleurExhaled;
        }
    }

    void Start()
    {
        gazeComponent = gazeComponent ? gazeComponent : GetComponent<GazeAware>();
    }

    private void Update()
    {
        if (souffleurAffected)
        {
            if (gazeComponent.HasGazeFocus)
            {
                if (isInhaled && isExhaled)
                {
                    Debug.LogError("ce superhéro arrive à inspirer et expirer en même temps, c'est pas trop possible, va régler le souffleur fdp");
                }
                if (isInhaled)
                {
                    GameEvents.Instance.CtrlAltInputSent(souffleurInhaledInput, isInhaled);
                    GameEvents.Instance.CtrlAltInputSent(myInput, false);
                }
                else if (isExhaled)
                {
                    GameEvents.Instance.CtrlAltInputSent(myInput, isExhaled);
                    GameEvents.Instance.CtrlAltInputSent(souffleurInhaledInput, false);
                }
                else
                {
                    GameEvents.Instance.CtrlAltInputSent(souffleurInhaledInput, isInhaled);
                    GameEvents.Instance.CtrlAltInputSent(myInput, isExhaled);
                }
            }
        }
        else
        {
            GameEvents.Instance.CtrlAltInputSent(myInput, gazeComponent.HasGazeFocus);
        }
    }

    void IsSouffleurInhaled(bool souffleurState)
    {
        isInhaled = souffleurState;
    }

    void IsSouffleurExhaled(bool souffleurState)
    {
        isExhaled = souffleurState;
    }
}
