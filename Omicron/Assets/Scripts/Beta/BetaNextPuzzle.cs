using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaNextPuzzle : MonoBehaviour
{
    [SerializeField] private BetaLevelManager betaLevelManager;
    [SerializeField] private Text debugText;
    private GameManager gameManager;
    private BetaSetMaxMagnets betaSetMaxMagnets;
    private float triggerTime;                          // Time for calculating length south magnet is in goal trigger zone
    [SerializeField] private float maxTriggerDuration;  // Max specified time south magnet must be in goal trigger zone
    [SerializeField] private float minimumVel;          // Minimum specified velocity south magnet must be when in the goal trigger zone

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter(Collider col)
    {
        // Once south magnet enters goal area, start counter
        if (col.CompareTag("SouthMagnet"))
        {
            triggerTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        // If the south magnet stays in the goal trigger zone
        // Check its velocity
        // If it is below the minimum velocuty
        // go to the next puzzle
        if (col.CompareTag("SouthMagnet"))
        {
            float colVel = col.GetComponent<Rigidbody>().velocity.magnitude;
            
            if (triggerTime + maxTriggerDuration < Time.time && colVel <= minimumVel)
            {
                // Note: Add UI here for countdown if so desired
                WaitForNextPuzzle();
            }
        }
    }

    // Method for going to the next puzzle
    // Sets MaxPlaceableMagnets in the beta level manager to the MaxMagnets of the next puzzle
    private void WaitForNextPuzzle()
    {
        gameManager.NextPuzzle();
        betaSetMaxMagnets = GameObject.Find(gameManager.FindActivePuzzle().name).GetComponent<BetaSetMaxMagnets>();   
        betaLevelManager.MaxPlaceableMagnets = betaSetMaxMagnets.maxMagnets;
        betaLevelManager.Reset();
        betaLevelManager.MagnetAttach();
    }
}
