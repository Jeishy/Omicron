using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeltaSetMaxPhotons : MonoBehaviour
{
    public int MaxPhotons;

    private DeltaLevelManager _deltaManager;

    private void Start()
    {
        _deltaManager = GameObject.Find("DeltaLevelManager").GetComponent<DeltaLevelManager>();
        // Set new max photons after 0.1 seconds
        StartCoroutine(SetNewMaxPhotons());
    }

    private IEnumerator SetNewMaxPhotons()
    {
        yield return new WaitForSeconds(0.1f);
        // Set new max shootable photons amount when going to this puzzle
        _deltaManager.MaxShootablePhotons = MaxPhotons;
    }
}
