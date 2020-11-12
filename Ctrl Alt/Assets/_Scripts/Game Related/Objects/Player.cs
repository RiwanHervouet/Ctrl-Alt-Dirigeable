using UnityEngine;
using System.Collections.Generic;

public class Player : MapObject
{
    private bool alreadyGotHit;
    private bool randomDirection;
    private int updatesUntilICanGetHitAgain;

    private Vector2 nextRelativePositionGoal;

    private Vector2[] orthographicDirections;

    protected override void InitObject()
    {
        xPosition = GameManager.Instance.xPlayerSpawn;
        yPosition = GameManager.Instance.yPlayerSpawn;
        type = physicalObjectType.player;

        #region Intialization
        alreadyGotHit = true;
        randomDirection = false;
        updatesUntilICanGetHitAgain = 0;

        nextRelativePositionGoal = new Vector2(1, 0);

        nextRelativePosition = new Vector2(1, 0);

        orthographicDirections = new Vector2[8];
        orthographicDirections[0] = new Vector2(0, 1);
        orthographicDirections[1] = new Vector2(1, 1);
        orthographicDirections[2] = new Vector2(1, 0);
        orthographicDirections[3] = new Vector2(1, -1);
        orthographicDirections[4] = new Vector2(0, -1);
        orthographicDirections[5] = new Vector2(-1, -1);
        orthographicDirections[6] = new Vector2(-1, 0);
        orthographicDirections[7] = new Vector2(-1, 1);
        #endregion

        GameEvents.Instance.OnPlayerGettingHit += PlayerHit;
        GameEvents.Instance.OnNextPlayerUpdate += ResetHitCooldown;
        GameEvents.Instance.OnShipRepaired += RepairDirection;

        base.InitObject();
    }
    protected override void DisableObject()
    {
        GameEvents.Instance.OnPlayerGettingHit -= PlayerHit;
        GameEvents.Instance.OnNextPlayerUpdate -= ResetHitCooldown;
        GameEvents.Instance.OnShipRepaired -= RepairDirection;
    }
    protected override void UpdateObject()
    {
        nextRelativePosition = GameEvents.Instance.PlayerDirectionChange() == Vector2.zero ? ShipTurning(nextRelativePositionGoal) : ShipTurning(GameEvents.Instance.PlayerDirectionChange());

        AdaptGraphics();
        base.UpdateObject();
    }

    void AdaptGraphics()
    {
        if (nextRelativePosition == new Vector2(1, 0))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0) };
        }
        else if (nextRelativePosition == new Vector2(1, 1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(-1, -1) };
        }
        else if (nextRelativePosition == new Vector2(1, -1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(-1, 1) };
        }
        else if (nextRelativePosition == new Vector2(0, 1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(0, -1) };
        }
        else if (nextRelativePosition == new Vector2(0, -1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1) };
        }
        else if (nextRelativePosition == new Vector2(-1, 1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(1, -1) };
        }
        else if (nextRelativePosition == new Vector2(-1, -1))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(1, 1) };
        }
        else if (nextRelativePosition == new Vector2(-1, 0))
        {
            graphics = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0) };
        }
    }

    void PlayerHit(List<physicalObjectType> objectHit) // Hit du bord de map est pas encore registered
    {
        if (objectHit.Contains(physicalObjectType.mountain))
        {
            if (!alreadyGotHit)
            {
                // Animation + damage effects
                updatesUntilICanGetHitAgain = GameManager.Instance.invincibilityDelay;
                alreadyGotHit = true;
                for (int i = 0; i < orthographicDirections.Length; i++)
                {
                    if (nextRelativePosition == orthographicDirections[i])
                    {
                        nextRelativePosition = i < 4 ? orthographicDirections[i + 4] : orthographicDirections[i - 4];
                        nextRelativePositionGoal = nextRelativePosition;
                        break;
                    }
                }
            }
        }
        else if (objectHit.Contains(physicalObjectType.lightning)) /////////////////////////////////////// pas encore discuté
        {
            if (!alreadyGotHit)
            {
                randomDirection = true;
                // animation
            }
        }
        else //bord de map hit
        {
            if (yPosition < 1)
            {
                yPosition = 5;
                nextRelativePosition = new Vector2(0, 1);
                nextRelativePositionGoal = nextRelativePosition;
            }
            else if (yPosition > GameManager.Instance.mapScript.fullMap.GetLength(1) - 2)
            {
                yPosition = GameManager.Instance.mapScript.fullMap.GetLength(1) - 5;
                nextRelativePosition = new Vector2(0, -1);
                nextRelativePositionGoal = nextRelativePosition;
            }
            else if (xPosition < 1)
            {
                xPosition = 5;
                nextRelativePosition = new Vector2(1, 0);
                nextRelativePositionGoal = nextRelativePosition;
            }
            else if (xPosition > GameManager.Instance.mapScript.fullMap.GetLength(0) - 2)
            {
                xPosition = GameManager.Instance.mapScript.fullMap.GetLength(0) - 5;
                nextRelativePosition = new Vector2(-1, 0);
                nextRelativePositionGoal = nextRelativePosition;
            }
            else
            {
                Debug.LogWarning("Je suis censé avoir touché le bord de la map mais non... J'identifie probablement mal un objet.");
            }
        }

        if (!alreadyGotHit)
        {
            updatesUntilICanGetHitAgain = GameManager.Instance.invincibilityDelay;
            alreadyGotHit = true;
        }
    }

    void ResetHitCooldown()
    {
        if (updatesUntilICanGetHitAgain > 0)
        {
            updatesUntilICanGetHitAgain--;
        }
        else
        {
            alreadyGotHit = false;
            //animation damage taken
        }
    }

    Vector2 ShipTurning(Vector2 _nextRelativePositionGoal)
    {
        nextRelativePositionGoal = _nextRelativePositionGoal;

        if (randomDirection) RandomizeDirection();

        if (Vector2.SignedAngle(nextRelativePosition, nextRelativePositionGoal) == 0)
        {
            return nextRelativePosition;
        }
        else if (Vector2.SignedAngle(nextRelativePosition, nextRelativePositionGoal) < 0)
        {
            if (nextRelativePosition == orthographicDirections[orthographicDirections.Length - 1])
            {
                return orthographicDirections[0];
            }
            else
            {
                for (int i = 0; i < orthographicDirections.Length; i++)
                {
                    if (nextRelativePosition == orthographicDirections[i])
                    {
                        return orthographicDirections[i + 1];
                    }
                }
                return Vector2.zero;
            }
        }
        else
        {
            if (nextRelativePosition == orthographicDirections[0])
            {
                return orthographicDirections[orthographicDirections.Length - 1];
            }
            else
            {
                for (int i = 0; i < orthographicDirections.Length; i++)
                {
                    if (nextRelativePosition == orthographicDirections[i])
                    {
                        return orthographicDirections[i - 1];
                    }
                }
                return Vector2.zero;
            }
        }

    }

    private void RandomizeDirection()
    {
        nextRelativePositionGoal = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        while (nextRelativePositionGoal != new Vector2(0, 0))
        {
            nextRelativePositionGoal = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }
    }
    public void RepairDirection()
    {
        randomDirection = false;
    }

    #region Useful but not needed to be seen
    public Player(int xPositionInit, int yPositionInit, physicalObjectType objectType) : base(xPositionInit, yPositionInit, objectType)
    {

    }
    #endregion
}
