using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpsilonCheckPuzzleComplete : MonoBehaviour
{
    [HideInInspector] public bool IsPuzzleCompleted;

    [SerializeField] private float _timeTillNextPuzzle;

    private bool _isNextPuzzle;
    private GameObject[] nuclei;
    private EpsilonLevelManager _epsilonManager;


    // Start is called before the first frame update
    void Start()
    {
        IsPuzzleCompleted = false;
        _isNextPuzzle = false;
        _epsilonManager = GameObject.Find("EpsilonLevelManager").GetComponent<EpsilonLevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if puzzle is complete
        CheckIfPuzzleComplete();

        // If puzzle is complete run wait for puzzle coroutine
        if (IsPuzzleCompleted && !_isNextPuzzle)
        {
            _isNextPuzzle = true;
            StopAllCoroutines();
            StartCoroutine(WaitForNextPuzzle());
        }
    }

    private void CheckIfPuzzleComplete()
    {
        // Get all nuclei in puzzle
        nuclei = GameObject.FindGameObjectsWithTag("Nucleus");
        GameObject activePuzzle = GameManager.Instance.FindActivePuzzle();
        if (activePuzzle.name != null)
        {
            if (activePuzzle.name == "4")
            {
                foreach (GameObject atomNuclei in nuclei)
                {
                    EpsilonAtomNucleus epsilonAtomNucleus = atomNuclei.GetComponent<EpsilonAtomNucleus>();
                    if (epsilonAtomNucleus != null)
                    {
                        if (epsilonAtomNucleus.IsParticleCreated)
                        {
                            IsPuzzleCompleted = true;
                        }
                        else
                        {
                            IsPuzzleCompleted = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject nucleus in nuclei)
                {
                    // Check if all nuclei in puzzle are 
                    EpsilonNucleus epsilonNucleus = nucleus.GetComponent<EpsilonNucleus>();
                    if (epsilonNucleus.IsParticleCreated)
                    {
                        IsPuzzleCompleted = true;
                    }
                    else
                    {
                        IsPuzzleCompleted = false;
                        break;
                    }
                }
            }
        }
    }

    private IEnumerator WaitForNextPuzzle()
    {
        // Play particle created sound
        AudioManager.Instance.Play("ParticleCreated");
        Debug.Log("Puzzle completed");
        yield return new WaitForSeconds(_timeTillNextPuzzle);
        GameObject nextPuzzle = GameManager.Instance.FindNextPuzzle(GameManager.Instance.FindActivePuzzle());

        // If at the end of the level, trigger level completed
        if (nextPuzzle == null)
            GameManager.Instance.LevelCompleted();
        // Else load the next puzzle
        else 
            GameManager.Instance.NextPuzzle();
        
        // Set number of quarks and baryons used back to 0
        _epsilonManager.NumQuarksUsed = 0;
        _epsilonManager.NumBaryonsUsed = 0;
        _isNextPuzzle = false;
        IsPuzzleCompleted = false;
    }
}
