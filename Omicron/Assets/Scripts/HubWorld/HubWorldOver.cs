using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldOver : MonoBehaviour
{
    private HubWorldManager _hubManager;
    private GameManager _gameManager;

    private void OnEnable()
    {
        Setup();
        _hubManager.OnOver += Over;
        _hubManager.OnOver += ShowStats;
    }

    private void OnDisable()
    {
        _hubManager.OnOver -= Over;
        _hubManager.OnOver -= ShowStats;
    }

    private void Setup()
    {
        _hubManager = GetComponent<HubWorldManager>();
    }

    private void Over(Collider uiElement)
    {
        // Increase the size of the panel that was selected
        Animator anim = uiElement.GetComponent<Animator>();
        anim.SetTrigger("Increase");
        // Play UI over sound
        AudioManager.Instance.Play("UIOver");
    }

    private void ShowStats(Collider uiElement)
    {
        // Get the stats panel gameobject and set active to true
        HubWorldStatsPanelHide statPanelHide = uiElement.gameObject.GetComponent<HubWorldStatsPanelHide>();
        GameObject statsPanel = statPanelHide.StatsPanel;
       // Get panel name
       string panelName = statsPanel.transform.parent.gameObject.name;
        // Check if level is completed
        // If its been completed, show the stats panel
        // else hide the stats panel
        Debug.Log(panelName);
        if (CheckIfLevelIsCompleted(panelName))
       {
            statsPanel.SetActive(true);
       }
    }

    private bool CheckIfLevelIsCompleted(string panelName)
    {
        bool isCompleted = false;
        // Check the string if it is [LevelName]
        // Store reference to array that contains information on if a level is completed
        switch (panelName)
        {
            case "AlphaLevel":
                isCompleted = GameManager.Instance.CompletedLevels[0];
                break;
            case "BetaLevel":
                isCompleted = GameManager.Instance.CompletedLevels[1];
                break;
            case "GammaaLevel":
                isCompleted = GameManager.Instance.CompletedLevels[2];
                break;
            case "DeltaLevel":
                isCompleted = GameManager.Instance.CompletedLevels[3];
                break;
            case "EpsilonLevel":
                isCompleted = GameManager.Instance.CompletedLevels[4];
                break;                                
        }
        // Return the boolean result
        return isCompleted;
    }
}
