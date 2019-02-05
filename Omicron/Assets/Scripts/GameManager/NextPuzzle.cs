using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class NextPuzzle : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Text debugText; 

    private void OnEnable()
    {
        Setup();
        gameManager.OnNextPuzzle += NextPzzle;
    }

    private void OnDisable()
    {
        gameManager.OnNextPuzzle -= NextPzzle;
    }

    private void Setup()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void NextPzzle()
    {
        // Find current active puzzle and set it to false
        // Find the next puzzle and set it to true
        // Note: update later to load with a transition
        GameObject lastPuzzle = gameManager.FindActivePuzzle();         
        lastPuzzle.SetActive(false);
        GameObject nextPuzzle = gameManager.FindNextPuzzle(lastPuzzle);
        nextPuzzle.SetActive(true);
    }
}
