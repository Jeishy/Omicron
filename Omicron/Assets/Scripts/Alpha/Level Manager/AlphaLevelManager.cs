using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLevelManager : MonoBehaviour {

    public delegate void AlphaLevelGravityChange(Vector3 gravity);
    public event AlphaLevelGravityChange OnGravityChange;

    public delegate void PlayerEventManagerAlpha();
    public event PlayerEventManagerAlpha OnBallShot;
    public event PlayerEventManagerAlpha OnResetBallPosition;
    public event PlayerEventManagerAlpha OnBallDropped;

    [HideInInspector] public bool IsGravityChanged;
    [HideInInspector] public bool IsBallDropped;
    [HideInInspector] public bool IsBallShot;

    private void Start()
    {
        GameObject ball = GameObject.Find("Ball");
        IsGravityChanged = false;
       if (ball != null)
       {
           ResetBallPosition();
       }
    }

    public void GravityChange(Vector3 gravity)
    {
        if (OnGravityChange != null)
        {
            OnGravityChange(gravity);
        }
    }
    
    public void BallDropped()
    {
        if (OnBallDropped != null)
        {
            OnBallDropped();
        }
    }

    public void BallShot()
    {
        if (OnBallShot != null)
        {
            OnBallShot();
        }
    }

    public void ResetBallPosition()
    {
        if (OnResetBallPosition != null)
        {
            OnResetBallPosition();
        }
    }
}
