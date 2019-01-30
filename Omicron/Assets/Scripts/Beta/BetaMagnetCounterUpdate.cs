using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaMagnetCounterUpdate : MonoBehaviour
{
    private GameObject magnetCounter;
    private BetaLevelManager betaManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        betaManager = GetComponent<BetaLevelManager>();
        magnetCounter = GameObject.Find("MagnetCounterText");
        magnetCounter.GetComponent<Text>().enabled = true;
        magnetCounter.GetComponent<Text>().text = betaManager.MaxPlaceableMagnets.ToString();
    }
}
