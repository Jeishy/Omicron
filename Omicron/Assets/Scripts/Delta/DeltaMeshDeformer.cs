using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DeltaMeshDeformer : MonoBehaviour
{
    Mesh _deformingMesh;
    Vector3[] _originalVertices, _displacedVertices,
                _vertexVelocities;

    // Start is called before the first frame update
    void Start()
    {
        _deformingMesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _deformingMesh.vertices;
        _displacedVertices = new Vector3[_originalVertices.Length];
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _displacedVertices[i] = _originalVertices[i];
        }

        _vertexVelocities = new Vector3[_originalVertices.Length];
    }
}
