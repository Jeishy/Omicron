using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private Transform _testSphereTrans;
    [SerializeField] private float _orbitSpeed;

    private Transform _trans;
    // Start is called before the first frame update
    void Start()
    {
        _trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _trans.RotateAround(_testSphereTrans.position, Vector3.up, _orbitSpeed * Time.deltaTime);
    }
}
