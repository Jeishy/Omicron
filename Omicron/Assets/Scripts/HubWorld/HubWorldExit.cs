using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldExit : MonoBehaviour
{
    private HubWorldManager hubManager;

    private void OnEnable()
    {
        Setup();
        hubManager.OnExit += Exit;
        hubManager.OnExit += HideStatsPanel;
    }

    private void OnDisable()
    {
        hubManager.OnExit -= Exit;
        hubManager.OnExit -= HideStatsPanel;
    }

    private void Setup()
    {
        hubManager = GetComponent<HubWorldManager>();
    }

    private void Exit(Collider uiElement)
    {
        // Decrease the size of the panel that was selected
        Animator anim = uiElement.GetComponent<Animator>();
        anim.SetTrigger("Decrease");
    }

    private void HideStatsPanel(Collider uiElement)
    {
        // Get the stats panel and deactive it
       HubWorldStatsPanelHide statPanelHide = uiElement.gameObject.GetComponent<HubWorldStatsPanelHide>();
       GameObject statsPanel = statPanelHide.StatsPanel;
       statsPanel.SetActive(false);
    }
}

