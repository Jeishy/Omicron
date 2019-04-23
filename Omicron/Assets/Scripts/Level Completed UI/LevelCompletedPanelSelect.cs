using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedPanelSelect : MonoBehaviour
{
    private LevelCompletedManager _levelCompleteManager;
    private GameManager _gameManager;

    private void OnEnable() 
    {
        Setup();
        _levelCompleteManager.OnSelect += Select;
    }

    private void OnDisable() 
    {
        _levelCompleteManager.OnSelect -= Select;        
    }

    private void Setup()
    {
        _levelCompleteManager = GetComponent<LevelCompletedManager>();
        _gameManager = GameManager.Instance;
    }

    private void Select(Collider col)
    {
        string panelName = col.gameObject.name;
        
        switch (panelName)
        {
            case "Next Level Panel":
                GoToNextLevel();
                break;
            case "Stats Panel":
                break;
            case "Back Panel":
                GoToHubWorld();
                break;
        }
    }

    private void GoToHubWorld()
    {
        // Load the hub world
        SceneManager.LoadScene(0);
    }

    private void OpenLevelStats()
    {

    }

    private void GoToNextLevel()
    {
        // Trigger the OnNextLevel event
        _gameManager.NextLevel();
    }
}
