﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelCompleted : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedPanels;

    private GameManager _gameManager;
    private string _currentLevel;

    private void OnEnable() 
    {
        Setup();
        _gameManager.OnLevelCompleted += LevelCompleted;
        _gameManager.OnNextLevel += AddLevelTime;
    }

    private void OnDisable() 
    {
        _gameManager.OnLevelCompleted -= LevelCompleted;
        _gameManager.OnNextLevel -= AddLevelTime;
    }

    private void Setup()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void LevelCompleted()
    {
        // Set IsLevelStarted bool to false
        _gameManager.IsLevelStarted = false;
        // Deactivate the selection visualizer in the scene
        DeactivateSelectionVisualizer();
        // Activate normal selection visualizer
        _gameManager.SelectionVisualizer.SetActive(true);
        // Deactivate all puzzle gameobjects
        DeactivateAllPuzzles();

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

    private void DeactivateSelectionVisualizer()
    {
        // Find the current level
        string levelName = _gameManager.FindActiveLevel();
        // Deactivate selection visualizer if not in the hub world
        if (levelName != "HubWorld")
        {
            // Find and deactivate the selection visualizer used in the level
            GameObject selectionVisualizer = GameObject.FindGameObjectWithTag("SelectionVisualizer");
            selectionVisualizer.SetActive(false);
        }
    }

    private void AddLevelTime()
    {
        // Get the level name and convert to of type level
        _currentLevel = _gameManager.FindActiveLevel();
        Level completedLevel = _gameManager.LevelStringToLevelType(_currentLevel);
        // Add
        _gameManager.AddCompletedLevelAndTime(completedLevel, _gameManager.LevelTimer);
        _gameManager.LevelTimer = 0f;
    }
}
