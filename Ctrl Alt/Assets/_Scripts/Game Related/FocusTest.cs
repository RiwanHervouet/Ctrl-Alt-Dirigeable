using System.Collections;
using UnityEngine;
//using Tobii.Gaming;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class FocusTest : MonoBehaviour
{
    #region Initiatlization
    //public GazeAware gaze;
    private Button button;
    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        //gaze = GetComponent<GazeAware>();
        button = GetComponent<Button>();

    }

    void Update()
    {
        /*if (gaze.HasGazeFocus)
        {
            //button.Select
        }*/
    }
}
