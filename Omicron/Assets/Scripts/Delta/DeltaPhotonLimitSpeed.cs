using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaPhotonLimitSpeed : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the speed of the photon
        float speed = _rb.velocity.magnitude;
        Debug.Log(speed);
        // Limit speed if it goes above the specified max speed
        if (_rb.velocity.magnitude > _maxSpeed)
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
    }
}
