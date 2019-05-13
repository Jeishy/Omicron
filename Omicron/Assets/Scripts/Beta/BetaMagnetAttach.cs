using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BetaMagnetAttach : MonoBehaviour
{
    private BetaLevelManager _betaManager;
    private Transform _magnetSpawnPointTrans;
    private ObjectPooler _objectPooler;
    [HideInInspector] public GameObject currentMagnet;  // The current magnet attached to the remote
    [HideInInspector] public bool IsMagnetAttached;     // Flag for case that the magnet is attached

    private void OnEnable()
    {
        Setup();
        _betaManager.OnMagnetAttach += MagnetAttach;
    }

    private void OnDisable()
    {
        _betaManager.OnMagnetAttach -= MagnetAttach;
    }

    private void Setup()
    {
        _betaManager = GetComponent<BetaLevelManager>();
        _objectPooler = GetComponent<ObjectPooler>();
        _magnetSpawnPointTrans = GameObject.FindGameObjectWithTag("BallSpawnPoint").transform;
        IsMagnetAttached = false;
    }

    private void MagnetAttach()
    {
        // If there is no attached magnet
        // Attach a magnet
        if (!IsMagnetAttached)
        {
            IsMagnetAttached = true;
            Vector3 magnetSpawnPointPos = _magnetSpawnPointTrans.position;                                          // Caches spawn point's position (BallSpawnPoint Gameobject)
            currentMagnet = _objectPooler.SpawnMagnetFromPool("Magnet", magnetSpawnPointPos, Quaternion.identity);  // Spawns and caches reference to magnet next in pool
            currentMagnet.GetComponent<Transform>().SetParent(_magnetSpawnPointTrans);
            currentMagnet.GetComponentInChildren<Canvas>().enabled = false;                                         // Turns off canvas for seeing magnet's range
            Debug.Log("Attaching magnet");
        }
    }
}
