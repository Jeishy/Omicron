using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonPuzzleComplete : MonoBehaviour
{
    [SerializeField][Range(0.1f, 3f)] private float _timeTillPuzzleComplete;

    private EpsilonLevelManager _epsilonManager;

    private void OnEnable() 
    {
        Setup();
        _epsilonManager.OnPuzzleComplete += PuzzleComplete;
    }

    private void OnDisable() 
    {
        _epsilonManager.OnPuzzleComplete -= PuzzleComplete;
    }

    private void Setup()
    {
        _epsilonManager = GetComponent<EpsilonLevelManager>();
    }

    private void PuzzleComplete()
    {
        StopAllCoroutines();
        // Call Wait to end puzzle coroutine
        StartCoroutine(WaitToNextPuzzle());
    }

    private IEnumerator WaitToNextPuzzle()
    {
        // Wait specified number of seconds to go to the next puzzle
        yield return new WaitForSeconds(_timeTillPuzzleComplete);       
        // Reset num quarks used to 0
        _epsilonManager.NumQuarksUsed = 0; 
        GameManager.Instance.NextPuzzle();
    }
}
