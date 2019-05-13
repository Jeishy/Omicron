using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldLevelIndicator : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelIndicators = new GameObject[2];

    private GameManager _gameManager;

    private void Start() 
    {
        // Get the game manager
        _gameManager = GameManager.Instance;
        // Get the name of the panel
        string panelName = gameObject.name;
        // Set indicator to false by default
        bool isCompleted = false;
        switch (panelName)
        {
            case "AlphaLevel":
                isCompleted = CheckIfLevelIsComplete(0);
                break;
            case "BetaLevel":
                isCompleted = CheckIfLevelIsComplete(1);
                break;
            case "GammaLevel":
                isCompleted = CheckIfLevelIsComplete(2);
                break;
            case "DeltaLevel":
                isCompleted = CheckIfLevelIsComplete(3);
                break;
            case "EpsilonLevel":
                isCompleted = CheckIfLevelIsComplete(4);
                break;                                
        }
        // Activate appropriate level indicator
        ActivateLevelIndicator(isCompleted);
    }
    
    private bool CheckIfLevelIsComplete(int level)
    {
        // Check if the level is completed from the completed levels array in the game manager
        bool isLevelCompleted = _gameManager.CompletedLevels[level];
        // If true, return 0 which activates the level completed indicator
        // else, return 1 which activates the level incomplete indicator
        if (isLevelCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ActivateLevelIndicator(bool isCompleted)
    {
        if (isCompleted)
        {
            // Set the level completed indicator to true
            _levelIndicators[0].SetActive(true);
            // Set the level incomplete indicator to false
            _levelIndicators[1].SetActive(false);
        }
        else
        {
            // Set the level incomplete indicator to true
            _levelIndicators[1].SetActive(true);
            // Set the level completed indicator to false
            _levelIndicators[0].SetActive(false);
        }
    }
}
