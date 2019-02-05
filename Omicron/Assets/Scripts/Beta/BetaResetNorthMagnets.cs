using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BetaResetNorthMagnets : MonoBehaviour
{
    private BetaLevelManager betaManager;
    [SerializeField] private Text debugText;
    private GameObject[] northMagnetsPlaced;
    [HideInInspector] public GameObject[] southMagnetGameObjects;
    [HideInInspector] public GameObject[] magnetSpawnPoints; 
    private void OnEnable()
    {
        Setup();
        betaManager.OnResetMagnets += ResetNorthMagnets; 
    }

    private void OnDisable()
    {
        betaManager.OnResetMagnets -= ResetNorthMagnets; 
    }

    private void Setup()
    {
        betaManager = GetComponent<BetaLevelManager>();
    }

    private void ResetNorthMagnets()
    {
        // Find all placed north magnets in the map
        northMagnetsPlaced = GameObject.FindGameObjectsWithTag("NorthMagnet");
        // If there are magnets that have been placed
        // Cycle through northMagnetsPlaced array and set their velocities to 0
        // Set gameobjects to false
        if (northMagnetsPlaced.Length > 0)
        {
            foreach (GameObject northMagnet in northMagnetsPlaced)
            {
                northMagnet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                northMagnet.SetActive(false);
            }
        }
        else
        {
            debugText.text = "No north magnets in the puzzle";
        }

        // Reset ballsPlaced variable in BetaMagnetPlacement class
        betaManager.Reset();
        // Attach magnet to oculus go remote
        betaManager.MagnetAttach();
    }
}
