using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaNextPuzzle : MonoBehaviour
{
    [SerializeField] private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Text debugText;
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

    /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameManager.NextPuzzle();
            alphaLevelManager.ResetBallPosition();

        }
    }*/
}
