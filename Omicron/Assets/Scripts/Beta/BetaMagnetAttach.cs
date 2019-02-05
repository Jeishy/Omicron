using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BetaMagnetAttach : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private Transform magnetSpawnPointTrans;
    private BetaMagnetPooler bMagnetPooler;
    [HideInInspector] public GameObject currentMagnet;  // The current magnet attached to the remote
    [HideInInspector] public bool IsMagnetAttached;     // Flag for case that the magnet is attached

    private void OnEnable()
    {
        Setup();
        betaManager.OnMagnetAttach += MagnetAttach;
    }

    private void OnDisable()
    {
        betaManager.OnMagnetAttach -= MagnetAttach;
    }

    private void Setup()
    {
        betaManager = GetComponent<BetaLevelManager>();
        bMagnetPooler = GetComponent<BetaMagnetPooler>();
        magnetSpawnPointTrans = PlayerControllerReferences.Instance.BallSpawnPoint;
    }

    private void MagnetAttach()
    {
        // If there is no attached magnet
        // Attach a magnet
        if (!IsMagnetAttached)
        {
            IsMagnetAttached = true;
            Vector3 magnetSpawnPointPos = magnetSpawnPointTrans.position;                                           // Caches spawn point's position (BallSpawnPoint Gameobject)
            currentMagnet = bMagnetPooler.SpawnMagnetFromPool("Magnet", magnetSpawnPointPos, Quaternion.identity);  // Spawns and caches reference to magnet next in pool
            currentMagnet.GetComponent<Transform>().SetParent(magnetSpawnPointTrans);
            currentMagnet.GetComponentInChildren<Canvas>().enabled = false;                                         // Turns off canvas for seeing magnet's range
        }
    }
}
