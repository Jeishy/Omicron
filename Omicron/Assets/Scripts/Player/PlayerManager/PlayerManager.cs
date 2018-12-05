using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public delegate void PlayerEventManager();
    public event PlayerEventManager OnBallDropped;
    public event PlayerEventManager OnBallThrown;
    public event PlayerEventManager OnResetPosition;

    [HideInInspector] public bool IsBallThrown;

    public void BallDropped()
    {
        if (OnBallDropped != null)
        {
            OnBallDropped();
        }
    }

    public void BallThrown()
    {
        if (OnBallThrown != null)
        {
            OnBallThrown();
        }
    }

    public void ResetPosition()
    {
        if (OnResetPosition != null)
        {
            OnResetPosition();
        }
    }
}
