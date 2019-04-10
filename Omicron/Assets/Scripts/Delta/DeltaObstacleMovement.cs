using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaObstacleMovement : MonoBehaviour
{
    [SerializeField] float _bobAmount;
    [SerializeField] float _rotationSpeed;

    private Transform _trans;

    // Start is called before the first frame update
    void Start()
    {
        _trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Bob the obstacle up and down
        _trans.Translate(0.0f, Mathf.Sin(Time.time) * Time.deltaTime * _bobAmount, 0.0f);
        // Rotate the obstacle
        _trans.Rotate(0.0f, Time.deltaTime * _rotationSpeed, 0.0f);
    }
}
