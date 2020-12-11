using System;
using System.Collections;
using UnityEngine;

public class LightningPrep : MonoBehaviour
{
    #region Initialization
    private Vector2[] animationFrame1 = new Vector2[]
    {
        new Vector2(3, 0),
        new Vector2(2, 1),
        new Vector2(1, 2),
        new Vector2(0, 3),
        new Vector2(-1, 2),
        new Vector2(-2, 1),
        new Vector2(-3, 0),
        new Vector2(-2, -1),
        new Vector2(-1, -2),
        new Vector2(0, -3),
        new Vector2(1, -2),
        new Vector2(2, -1)
    };
    private Vector2[] animationFrame2 = new Vector2[]
    {
        new Vector2(-2, 0),
        new Vector2(-1, -1),
        new Vector2(0, -2),
        new Vector2(1, -1),
        new Vector2(2, 0),
        new Vector2(1, 1),
        new Vector2(0, 2),
        new Vector2(-1, 1)
    };
    private Vector2[] animationFrame3 = new Vector2[]
    {
        new Vector2(-1, 0),
        new Vector2(0, -1),
        new Vector2(0, 1),
        new Vector2(1, 0)
    };
    private Vector2[] animationFrame4 = new Vector2[]
    {
        new Vector2(0,0)
    };
    private Vector2[] animationFrame5 = new Vector2[0];
    private Vector2[] animationFrame6 = new Vector2[]
    {
        new Vector2(0,0)
    };
    private Vector2[] animationFrame7 = new Vector2[0];
    private Vector2[] animationFrame8 = new Vector2[]
    {
        new Vector2(0,0)
    };
    MapObject lightningPrep;
    MapObject lightningParent;
    public bool finishingAnimationNextFrame;
    int animationFrameIndex = 0;
    int animationLength = 0;
    bool initDone = false;
    #endregion
    void Start()
    {
        initDone = false;
    }


    private void Init()
    {
        initDone = true;
        finishingAnimationNextFrame = false;
        lightningPrep = lightningPrep ? lightningPrep : GetComponent<MapObject>();
        lightningPrep = lightningPrep ? lightningPrep : transform.parent.GetComponent<MapObject>();
        lightningPrep.xPosition = lightningParent.xPosition;
        lightningPrep.yPosition = lightningParent.yPosition;
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnJustBeforeNextRefresh += UpdateLightningPrepGraphics;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnJustBeforeNextRefresh -= UpdateLightningPrepGraphics;
    }

    public void UpdateLightningPrepGraphics()
    {
        if (initDone)
        {
            if (lightningPrep.isActive)
            {
                if (finishingAnimationNextFrame)
                {
                    lightningPrep.isActive = false;
                }
                GetGraphics();
                animationFrameIndex++;
            }
        }
    }
    public void StartAnimation(int animationLength)
    {
        InitAnimation();
        this.animationLength = animationLength;
    }

    private void InitAnimation()
    {
        animationFrameIndex = 1;
        finishingAnimationNextFrame = false;
        lightningPrep.isActive = true;
    }

    private void GetGraphics()
    {
        if (animationLength == animationFrameIndex - 1)
        {
            finishingAnimationNextFrame = true;
        }
        switch (animationFrameIndex)
        {
            case 1:
                lightningPrep.graphicsMiddleAltitude = animationFrame1;
                break;
            case 2:
                lightningPrep.graphicsMiddleAltitude = animationFrame2;
                break;
            case 3:
                lightningPrep.graphicsMiddleAltitude = animationFrame3;
                break;
            case 4:
                lightningPrep.graphicsMiddleAltitude = animationFrame4;
                break;
            case 5:
                lightningPrep.graphicsMiddleAltitude = animationFrame5;
                break;
            case 6:
                lightningPrep.graphicsMiddleAltitude = animationFrame6;
                break;
            case 7:
                lightningPrep.graphicsMiddleAltitude = animationFrame7;
                break;
            case 8:
                lightningPrep.graphicsMiddleAltitude = animationFrame8;
                break;
            default:
                lightningPrep.graphicsMiddleAltitude = new Vector2[0];
                break;
        }
    }


    private void Update()
    {
        if (!initDone)
        {
            if (lightningPrep != null)
            {
                Init();
            }
        }
    }
}
