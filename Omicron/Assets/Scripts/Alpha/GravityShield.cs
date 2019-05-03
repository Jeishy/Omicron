using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShield : MonoBehaviour
{
    [SerializeField] private Vector3 gravityChange;

    private AlphaLevelManager _alphaLevelManager;

    private void Start()
    {
        _alphaLevelManager = GameObject.Find("AlphaLevelManager").GetComponent<AlphaLevelManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            // If ball enters the gravity shield trigger, change gravity by specified Vector3
            _alphaLevelManager.GravityChange(gravityChange);
            // Play gravity enter sound
            AudioManager.Instance.Play("ShieldEnter");
        }
    }
}
