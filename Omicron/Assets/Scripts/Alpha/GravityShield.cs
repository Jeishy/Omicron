using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShield : MonoBehaviour
{
    [SerializeField] private AlphaLevelManager alphaLevelManager;
    [SerializeField] private Vector3 gravityChange;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            // If ball enters the gravity shield trigger, change gravity by specified Vector3
            alphaLevelManager.GravityChange(gravityChange);
        }
    }
}
