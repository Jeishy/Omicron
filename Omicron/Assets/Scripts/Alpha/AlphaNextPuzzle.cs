using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaNextPuzzle : MonoBehaviour
{
    [SerializeField] private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Text debugText;    // Used for debugging purposes
    private GameManager gameManager;
    //[SerializeField] private Text debugText;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            // If the next calculated puzzle return as null go to the next level
            if (gameManager.FindNextPuzzle(gameManager.FindActivePuzzle()) == null)
            {
                // Note: Open puzzle finished window
                // allow player to go to next level or back to main menu here
                debugText.text = "Going to next level";
                gameManager.NextLevel();
            }
            else
            {
                // Go to the next puzzle if there is one
                // and reset the balls position
                gameManager.NextPuzzle();
                alphaLevelManager.ResetBallPosition();
            }
        }
    }

    // Used for debugging; to cycle through puzzles in the game
    private void Update()
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
    }
}
