using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;

    private void OnEnable()
    {
        Setup();
        gameManager.OnNextLevel += LevelNext;
    }

    private void OnDisable()
    {
        gameManager.OnNextLevel += LevelNext;
    }

    private void Setup()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void LevelNext()
    {
        // Change to going giving an option to go back to
        // hub world or next puzzle
        string currentLevel = gameManager.FindActiveLevel();
        switch (currentLevel)
        {
            case "AlphaLevel":
                currentLevel = "BetaLevel";
                break;
            case "BetaLevel":
                currentLevel = "GammaLevel";
                break;
            case "GammaLevel":
                currentLevel = "DeltaLevel";
                break;
            case "DeltaLevel":
                currentLevel = "EpsilonLevel";
                break;
            case "EpsilonLevel":
                // Finished the game!
                break;
        }

        SceneManager.LoadScene(currentLevel);
    }
}
