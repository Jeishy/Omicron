using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonPuzzleComplete : MonoBehaviour
{
    [SerializeField][Range(0.1f, 3f)] private float _timeTillPuzzleComplete;

    private EpsilonLevelManager _epsilonManager;
    private bool _isPuzzleCompleted;
    private GameObject[] nuclei;

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
        _isPuzzleCompleted = false;
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
    
    private void CheckIfPuzzleComplete()
    {
        // Get all nuclei in puzzle
        nuclei = GameObject.FindGameObjectsWithTag("Nucleus");

        foreach (GameObject nucleus in nuclei)
        {
            // Check if all nuclei in puzzle are 
            EpsilonNucleus deltaNucleus = nucleus.GetComponent<EpsilonNucleus>();
            if (deltaNucleus.IsQuarkCreated)
            {
                _isPuzzleCompleted = true;
            }
            else
            {
                _isPuzzleCompleted = false;
                break;
            }
        }
    }
}
