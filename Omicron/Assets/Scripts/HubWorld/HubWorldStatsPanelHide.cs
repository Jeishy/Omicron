using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldStatsPanelHide : MonoBehaviour
{
    public GameObject StatsPanel;

    // Start is called before the first frame update
    void Start()
    {
        StatsPanel.SetActive(false);
    }
}
