using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaLevelManager : MonoBehaviour
{
    public delegate void PlayerEventManagerGamma();
    public event PlayerEventManagerGamma OnRestartPuzzle;
    public event PlayerEventManagerGamma OnPuzzleStart;

    private bool isParticlesSpawned;
    [HideInInspector] public bool IsParticlesSpawned
    {
        get {return isParticlesSpawned; }
        set { isParticlesSpawned = value; }
    }
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.FindAllPuzzles();           // Find all puzzles at the beginning of the level, and deactivate all but the first
    }

    public void RestartPuzzle()
    {
        if (OnRestartPuzzle != null)
        {
            OnRestartPuzzle();
        }
    }

    public void PuzzleStart()
    {
        if (OnPuzzleStart != null)
        {
            OnPuzzleStart();
        }
    }
}
