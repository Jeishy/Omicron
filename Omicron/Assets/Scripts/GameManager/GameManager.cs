﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Delegates and Events
    public delegate void GameEventManager();
    public event GameEventManager OnGamePaused;
    public event GameEventManager OnHubWorld;
    public event GameEventManager OnGameQuit;
    public event GameEventManager OnNextPuzzle;
    public event GameEventManager OnNextLevel;
    public event GameEventManager OnLevelRestart;
    #endregion 

    // Stores the current level that the game is on
    [HideInInspector] public string currentLevel;
    [HideInInspector] public static GameManager Instance = null;
    // Store the active puzzle in the level
    [HideInInspector] public GameObject[] activePuzzle;
    // Store a reference to all the puzzles in a level
    GameObject[] puzzles;
    // Stores the names of all the puzzles in a level as ints
    int[] puzzleInts;
    //[SerializeField] private Text debugText;

    #region Singleton
    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    private void Start()
    {
        // Finds all puzzles at beginning of level
        FindAllPuzzles();
    }

    // Function for finding all the puzzles in a level
    // Called at the beginning of each level
    public void FindAllPuzzles()
    {
        puzzles = GameObject.FindGameObjectsWithTag("Puzzles");
        // Sorts the array by int in ascending order
        // Names of puzzles are stored as ints
        puzzles = puzzles.OrderBy(p => int.Parse(p.name)).ToArray();

        // Loops through all indexes of puzzle array and sets
        // all puzzles to false except the first puzzle
        for (int i = 1; i <= puzzles.Length-1; i++)
        {
            puzzles[i].SetActive(false);
        }
    }

    // Function for finding the active puzzle in a level
    public GameObject FindActivePuzzle()
    {
        GameObject activePuzzle = GameObject.FindGameObjectWithTag("Puzzles");
        return activePuzzle;
    }

    // Function for finding the next puzzle
    // Note: FindActivePuzzle is used as a parameter for this function
    public GameObject FindNextPuzzle(GameObject lastPuzzle)
    {
        int puzzle = Convert.ToInt32(lastPuzzle.name);
        // Checks next calculated puzzle value is greater than number of puzzles
        // in the level
        // Returns null if there are no more puzzles
        // Returns the puzzle if there is a puzzle left
        if (puzzle+1 > puzzles.Length)
        {
            return null;
        }
        else
        {
            GameObject nextPuzzle = puzzles[puzzle];        
            return nextPuzzle;
        }
    }

    // Function for finding the active level
    public string FindActiveLevel()
    {
        string activeLevel = SceneManager.GetActiveScene().name;
        return activeLevel;
    }

    // Function for running all methods subcribed to the OnGamePaused event
    public void GamePaused()
    {
        if (OnGamePaused != null)
        {
            OnGamePaused();
        }
    }

    // Function for running all methods subcribed to the OnHubWorld event
    public void MainMenu()
    {
        if (OnHubWorld != null)
        {
            OnHubWorld();
        }
    }

    // Function for running all methods subcribed to the OnGameQuit event
    public void GameQuit()
    {
        if (OnGameQuit != null)
        {
            OnGameQuit();
        }
    }

    // Function for running all methods subcribed to the OnNextPuzzle event
    public void NextPuzzle()
    {
        if (OnNextPuzzle != null)
        {
            OnNextPuzzle();
        }
    }

    // Function for running all methods subcribed to the OnNextLevel event
    public void NextLevel()
    {
        if (OnNextLevel != null)
        {
            OnNextLevel();
        }
    }

    // Function for running all methods subcribed to the OnLevelRestart event
    public void LevelRestart()
    {
        if (OnLevelRestart != null)
        {
            OnLevelRestart();
        }
    }
}
