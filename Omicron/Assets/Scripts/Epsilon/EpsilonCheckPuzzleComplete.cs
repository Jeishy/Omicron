using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonCheckPuzzleComplete : MonoBehaviour
{
    [SerializeField] private float _timeTillNextPuzzle;

    private bool _isPuzzleCompleted;
    private GameObject[] nuclei;
    private EpsilonLevelManager _epsilonManager;


    // Start is called before the first frame update
    void Start()
    {
        _isPuzzleCompleted = false;
        // Get reference to the epsilon level manager
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if puzzle is complete
        CheckIfPuzzleComplete();

        // If puzzle is complete run wait for puzzle coroutine
        if (_isPuzzleCompleted)
        {
            _isPuzzleCompleted = false;
            StopAllCoroutines();
            StartCoroutine(WaitForNextPuzzle());
        }
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

    private IEnumerator WaitForNextPuzzle()
    {
        yield return new WaitForSeconds(_timeTillNextPuzzle);
        GameObject nextPuzzle = GameManager.Instance.FindNextPuzzle(GameManager.Instance.FindActivePuzzle());

        // If at the end of the level, trigger level completed
        if (nextPuzzle == null)
            GameManager.Instance.LevelCompleted();
        // Else load the next puzzle
        else 
            GameManager.Instance.NextPuzzle();
    }
}
