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
        _gameManager = GetComponent<GameManager>();
    }

    private void LevelCompleted()
    {
        // Deactivate all puzzle gameobjects
        DeactivateAllPuzzles();
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

    private void DeactivateAllPuzzles()
    {
        // Get all puzzles in the level
        GameObject[] puzzles = _gameManager.puzzles;
        // Set all puzzles to false in the puzzles array
        for (int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }
    }
}
