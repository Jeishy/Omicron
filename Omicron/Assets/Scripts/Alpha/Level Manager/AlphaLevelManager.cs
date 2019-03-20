using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaLevelManager : MonoBehaviour {

    #region Delegates and Events
    // Delegate and event for changes to level's gravity
    public delegate void AlphaLevelGravityChange(Vector3 gravity);
    public event AlphaLevelGravityChange OnGravityChange;

    // Delegate and events for alpha level
    public delegate void PlayerEventManagerAlpha();
    public event PlayerEventManagerAlpha OnBallShot;
    public event PlayerEventManagerAlpha OnResetBallPosition;
    public event PlayerEventManagerAlpha OnBallDropped;
    #endregion

    [HideInInspector] public bool IsGravityChanged;     // Flag for change in gravity
    [HideInInspector] public bool IsBallDropped;        // Flag for case that the ball is dropped (Goes too far from player)
    [HideInInspector] public bool IsBallShot;           // Flag for case that the ball is shot by player 
    private GameManager _gameManager;

    private void Start()
    {
        // Find the ball at the beginning of the game
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        // Set flag to false at start of level
        IsGravityChanged = false;
        // Reset balls position to be at front of remote
       if (ball != null)
       {
           ResetBallPosition();
       }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.FindAllPuzzles();           // Find all puzzles at the beginning of the level, and deactivate all but the first
    }

    // Function called for methods subscribed to OnGravityChange event
    public void GravityChange(Vector3 gravity)
    {
        if (OnGravityChange != null)
        {
            OnGravityChange(gravity);
        }
    }
    
    // Function called for methods subscribed to OnBallDropped event
    public void BallDropped()
    {
        if (OnBallDropped != null)
        {
            OnBallDropped();
        }
    }

    // Function called for methods subscribed to OnBallShot event
    public void BallShot()
    {
        if (OnBallShot != null)
        {
            OnBallShot();
        }
    }

    // Function called for methods subscribed to OnResetBallPosition event
    public void ResetBallPosition()
    {
        if (OnResetBallPosition != null)
        {
            OnResetBallPosition();
        }
    }
}
