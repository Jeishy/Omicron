using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameManagerNextPuzzle : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Text debugText; 

    private void OnEnable()
    {
        Setup();
        _gameManager.OnNextPuzzle += NextPuzzle;
    }

    private void OnDisable()
    {
        _gameManager.OnNextPuzzle -= NextPuzzle;
    }

    private void Setup()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void NextPuzzle()
    {
        // Find current active puzzle and set it to false
        // Find the next puzzle and set it to true
        // Note: update later to load with a transition
        GameObject lastPuzzle = _gameManager.FindActivePuzzle();         
        lastPuzzle.SetActive(false);
        GameObject nextPuzzle = _gameManager.FindNextPuzzle(lastPuzzle);
        nextPuzzle.SetActive(true);
    }
}
