using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaMagnetCounterUpdate : MonoBehaviour
{
    private Text magnetCounter;       // Variable for showing number of magnets available to be placed
    private BetaLevelManager betaManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        betaManager = GetComponent<BetaLevelManager>();
        magnetCounter = GameObject.Find("MagnetCounterText").GetComponent<Text>();
        magnetCounter.enabled = true;
        magnetCounter.GetComponent<Text>().text = betaManager.MaxPlaceableMagnets.ToString();   // Sets the magnetCounter to show max placeable magnets in current puzzle
    }
}
