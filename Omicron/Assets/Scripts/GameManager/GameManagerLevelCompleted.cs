using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelCompleted : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedPanels;

    private GameManager _gameManager;
    private void OnEnable() 
    {
        Setup();
        _gameManager.OnLevelCompleted += LevelCompleted;
    }

    private void OnDisable() 
    {
        _gameManager.OnLevelCompleted -= LevelCompleted;
    }

    private void Setup()
    {
        //_levelCompletedPanels.SetActive(false);
        _gameManager = GetComponent<GameManager>();
    }

    private void LevelCompleted()
    {
        // Pause the game
        Time.timeScale = 0f;
        // Show level completed canvas
        _levelCompletedPanels.SetActive(true);
        // Add level completed and level completed timer
        Level currentLevel = _gameManager.LevelStringToLevelType(_gameManager.FindActiveLevel());
        _gameManager.AddCompletedLevelAndTime(currentLevel, _gameManager.LevelTimer);

        // Reset level timer
        _gameManager.IsLevelStarted = false;
        _gameManager.LevelTimer = 0f;
    }
}
