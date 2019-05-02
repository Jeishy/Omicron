using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaSetMaxMagnets : MonoBehaviour
{
    public int maxMagnets;
    private BetaLevelManager _betaManager;
    // Start is called before the first frame update
    private void Start()
    {
        _betaManager = GameObject.Find("BetaLevelManager").GetComponent<BetaLevelManager>();
        // Run coroutine to wait for max magnets to be set in each puzzle
        StartCoroutine("SetMaxMagnets");
    }

    private IEnumerator SetMaxMagnets()
    {
        // Note: Find more efficient way of ensuring max magnets is set by first puzzle, PLEASE.
        yield return new WaitForSeconds(0.1f);
        _betaManager.MaxPlaceableMagnets = maxMagnets;
    }
}
