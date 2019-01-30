using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaSetMaxMagnets : MonoBehaviour
{
    public int maxMagnets;
    private BetaLevelManager betaManager;
    // Start is called before the first frame update
    private void Start()
    {
        betaManager = GameObject.Find("BetaLevelManager").GetComponent<BetaLevelManager>();
        StartCoroutine("SetMaxMagnets");
    }

    private IEnumerator SetMaxMagnets()
    {
        // Note: Find more efficient way of ensuring max magnets is set by first puzzle, PLEASE.
        yield return new WaitForSeconds(0.5f);
        betaManager.MaxPlaceableMagnets = maxMagnets;
    }
}
