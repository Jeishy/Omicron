﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaNextPuzzle : MonoBehaviour
{
    [SerializeField] private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Text debugText;    // Used for debugging purposes
    //[SerializeField] private Text debugText;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            // If the next calculated puzzle return as null go to the next level
            if (GameManager.Instance.FindNextPuzzle(GameManager.Instance.FindActivePuzzle()) == null)
            {
                // Call level completed method in the game manager
                GameManager.Instance.LevelCompleted();
            }
            else
            {
                // Go to the next puzzle if there is one
                // and reset the balls position
                GameManager.Instance.NextPuzzle();
                alphaLevelManager.ResetBallPosition();
            }
        }
    }

    // Used for debugging; to cycle through puzzles in the game
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameManager.FindNextPuzzle(gameManager.FindActivePuzzle()) == null)
            {
                debugText.text = "Going to next level";
                gameManager.NextLevel();
            }
            else
            {
                gameManager.NextPuzzle();
                alphaLevelManager.ResetBallPosition();
            }
        }
    }*/
}
