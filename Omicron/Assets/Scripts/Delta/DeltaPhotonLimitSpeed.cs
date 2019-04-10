using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonLimitSpeed : MonoBehaviour
{
    private DeltaLevelManager _deltaManager;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _deltaManager = GameObject.Find("DeltaLevelManager").GetComponent<DeltaLevelManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Limit speed if it goes above the speed of the photon when it was first shot
        if (_rb.velocity.magnitude > _deltaManager.MaxPhotonSpeed)
            _rb.velocity = _rb.velocity.normalized * _deltaManager.MaxPhotonSpeed;
    }
}
