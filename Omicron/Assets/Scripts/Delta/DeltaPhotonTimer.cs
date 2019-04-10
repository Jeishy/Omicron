using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonTimer : MonoBehaviour
{
    [HideInInspector] public bool HasMaxPhotonTimeIncreased;
    [HideInInspector] public bool IsPhotonShot;

    private float _maxPhotonTime;              // Max time that a photon can be alive for
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
        // Set max time to 1.5 seconds by default
        _maxPhotonTime = 1f;
        HasMaxPhotonTimeIncreased = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPhotonShot)
        {
            // Increment timer every frame
            _timer += Time.deltaTime;

            // If timer goes beyond max photon time, destroy the photon
            if (_timer >= _maxPhotonTime)
            {
                // P
                Destroy(gameObject);
            }
        }
    }

    public void AddTime(float time)
    {
        _maxPhotonTime += time;
    }
}
