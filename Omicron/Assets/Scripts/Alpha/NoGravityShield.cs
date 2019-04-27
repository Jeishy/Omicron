using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravityShield : MonoBehaviour
{
    [SerializeField] private bool _canChangeBackGravity;

    private bool _isGravityChanged;
    private AlphaLevelManager _alphaManager;
    private Vector3 _originalGravity;

    private void Start()
    {
        _alphaManager = GameObject.Find("AlphaLevelManager").GetComponent<AlphaLevelManager>();
        _originalGravity = Physics.gravity;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            Rigidbody ballRB = col.gameObject.GetComponent<Rigidbody>();
            if (!_isGravityChanged)
            {
                // Toggle bool if gravity can change back by passing through this shield
                if (_canChangeBackGravity)
                    _isGravityChanged = true;

                // Turn off gravity if ball hasnt already passed through the shield
                _alphaManager.GravityChange(Vector3.zero);
            }
            else if (_canChangeBackGravity)
            {
                _isGravityChanged  = false;
                // Turn pn gravity if ball has already passed through the shield
                _alphaManager.GravityChange(_originalGravity);
            }

        }
    }
}
