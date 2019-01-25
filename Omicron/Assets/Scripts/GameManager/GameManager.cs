using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public delegate void GameEventManager();
    public event GameEventManager OnGamePaused;
    public event GameEventManager OnGameQuit;
    public event GameEventManager OnNextPuzzle;
    public event GameEventManager OnRestartPuzzle;
    public event GameEventManager OnNextLevel;
    public event GameEventManager OnLevelRestart;

    [HideInInspector] public string currentLevel;
    [HideInInspector] public static GameManager Instance = null;
    [HideInInspector] public GameObject[] activePuzzle;
    GameObject[] puzzles;
    int[] puzzleInts;
    [SerializeField] private Text debugText;

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
    
    private void Start()
    {
        FindAllPuzzles();
    }

    public void FindAllPuzzles()
    {
        puzzles = GameObject.FindGameObjectsWithTag("Puzzles");
        puzzles = puzzles.OrderBy(p => int.Parse(p.name)).ToArray();

        for (int i = 1; i <= puzzles.Length-1; i++)
        {
            puzzles[i].SetActive(false);
        }
    }

    public GameObject FindActivePuzzle()
    {
        GameObject activePuzzle = GameObject.FindGameObjectWithTag("Puzzles");
        return activePuzzle;
    }

    public GameObject FindNextPuzzle(GameObject lastPuzzle)
    {
        int puzzle = Convert.ToInt32(lastPuzzle.name);
        GameObject nextPuzzle = puzzles[puzzle];
        return nextPuzzle;
    }

    public string FindActiveLevel()
    {
        string activeLevel = SceneManager.GetActiveScene().name;
        return activeLevel;
    }
    public void GamePaused()
    {
        if (OnGamePaused != null)
        {
            OnGamePaused();
        }
    }

    public void GameQuit()
    {
        if (OnGameQuit != null)
        {
            OnGameQuit();
        }
    }

    public void NextPuzzle()
    {
        if (OnNextPuzzle != null)
        {
            OnNextPuzzle();
        }
    }

    public void RestartPuzzle()
    {
        if (OnRestartPuzzle != null)
        {
            OnRestartPuzzle();
        }
    }

    public void NextLevel()
    {
        if (OnNextLevel != null)
        {
            OnNextLevel();
        }
    }

    public void LevelRestart()
    {
        if (OnLevelRestart != null)
        {
            OnLevelRestart();
        }
    }
}
