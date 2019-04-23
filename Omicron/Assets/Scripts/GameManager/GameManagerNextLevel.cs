using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerNextLevel : MonoBehaviour
{
    private GameManager _gameManager;
    private string _currentLevel;

    private void OnEnable()
    {
        Setup();
        _gameManager.OnNextLevel += LevelNext;
    }

    private void OnDisable()
    {
        _gameManager.OnNextLevel -= LevelNext;
    }

    private void Setup()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void LevelNext()
    {
        // Change to going giving an option to go back to
        // hub world or next puzzle
        _currentLevel = _gameManager.FindActiveLevel();
        switch (_currentLevel)
        {
            case "AlphaLevel":
                _currentLevel = "BetaLevel";
                break;
            case "BetaLevel":
                _currentLevel = "GammaLevel";
                break;
            case "GammaLevel":
                _currentLevel = "DeltaLevel";
                break;
            case "DeltaLevel":
                _currentLevel = "EpsilonLevel";
                break;
            case "EpsilonLevel":
                // Finished the game!
                break;
        }

        SceneManager.LoadScene(_currentLevel);
    }
}
