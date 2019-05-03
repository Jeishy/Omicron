using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonSetMaxParticles : MonoBehaviour
{
    [SerializeField][Range(3, 100)] private int _maxPuzzleQuarks;
    [SerializeField][Range(3, 100)] private int _maxPuzzleBaryons;

    private EpsilonLevelManager _epsilonManager;
    
    private void Start()
    {
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
        // Set new max photons after 0.5 seconds
        StartCoroutine(SetMaxQuarks());
    }

    private IEnumerator SetMaxQuarks()
    {
        yield return new WaitForSeconds(0.5f);
        // Set new max shootable photons amount when going to this puzzle
        _epsilonManager.MaxQuarks = _maxPuzzleQuarks;
        _epsilonManager.MaxBaryons = _maxPuzzleBaryons;
    }
}
