using System.Collections;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class EyeTrackerInput : MonoBehaviour
{
    #region Initialization
    public GazeAware gazeComponent;
    public Inputs.inputs myInput { 
        get
        {
            return _myInput;
        }
    }
    [SerializeField] private Inputs.inputs _myInput;
    #endregion


    void Start()
    {
        gazeComponent = gazeComponent ? gazeComponent : GetComponent<GazeAware>();
    }

    private void Update()
    {
        GameEvents.Instance.CtrlAltInputSent(myInput, gazeComponent.HasGazeFocus);
    }
}
