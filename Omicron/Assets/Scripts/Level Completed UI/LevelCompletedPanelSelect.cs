using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedPanelSelect : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedPanel;
    [SerializeField] private LevelCompletedManager _levelCompleteManager;
    
    private GameManager _gameManager;

    private void OnEnable() 
    {
        _levelCompleteManager.OnSelect += Select;
    }

    private void OnDisable() 
    {
        _levelCompleteManager.OnSelect -= Select;        
    }

    private void Select(Collider col)
    {
        // Hide level completed panel
        _levelCompletedPanel.SetActive(false);
        string panelName = col.gameObject.name;
        
        switch (panelName)
        {
            case "Next Level Panel":
                GoToNextLevel();
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

    private void GoToNextLevel()
    {
        // Call all methods subscribed to the OnLevelStart event
        GameManager.Instance.LevelStart();
        // Trigger the OnNextLevel event
        GameManager.Instance.NextLevel();
    }
}
