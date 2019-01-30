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
        northMagnetsPlaced = GameObject.FindGameObjectsWithTag("NorthMagnet");
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

        betaManager.Reset();
        betaManager.MagnetAttach();
    }
}
