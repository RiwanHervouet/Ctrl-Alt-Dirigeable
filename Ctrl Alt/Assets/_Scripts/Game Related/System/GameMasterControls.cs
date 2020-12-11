using System.Collections;
using UnityEngine;

public class GameMasterControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("MJ Speed Fast"))
        {
            GameTime.Instance.ChangePlayerUpdateRate(GameTime.playerSpeed.FAST);
        }
        else if (Input.GetButtonDown("MJ Speed Medium"))
        {
            GameTime.Instance.ChangePlayerUpdateRate(GameTime.playerSpeed.MEDIUM);
        }
        else if (Input.GetButtonDown("MJ Speed Slow"))
        {
            GameTime.Instance.ChangePlayerUpdateRate(GameTime.playerSpeed.SLOW);
        }

        if (Input.GetButtonDown("MJ Altitude Top"))
        {
            GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.TopAltitude;
        }
        else if (Input.GetButtonDown("MJ Altitude Middle"))
        {
            GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.MiddleAltitude;
        }
        else if (Input.GetButtonDown("MJ Altitude Bottom"))
        {
            GameManager.Instance.player.nextAltitudeGoal = GameManager.altitudes.BottomAltitude;
        }
    }
}
