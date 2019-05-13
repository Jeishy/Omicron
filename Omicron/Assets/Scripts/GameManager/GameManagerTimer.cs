using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTimer : MonoBehaviour
{
    private GameManager _gameManager;

    private void OnEnable()
    {
        Setup();
        _gameManager.OnLevelStart += ActivateTimer;
    }

    private void OnDisable() 
    {
        _gameManager.OnLevelStart -= ActivateTimer;
    }

    private void Setup()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void ActivateTimer()
    {
        // Set local is level started bool to true
        _gameManager.IsLevelStarted = true;
    }

    private void Update() 
    {
        // If the bool is set to true, begin incrementing the timer
        if (_gameManager.IsLevelStarted)
        {
            _gameManager.LevelTimer += Time.deltaTime;
        }    
    }
}
