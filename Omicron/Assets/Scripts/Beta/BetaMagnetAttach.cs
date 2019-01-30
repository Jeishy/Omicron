using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BetaMagnetAttach : MonoBehaviour
{
    private BetaLevelManager betaManager;
    private Transform magnetSpawnPointTrans;
    private BetaMagnetPooler bMagnetPooler;
    [HideInInspector] public GameObject currentMagnet;
    [HideInInspector] public float MagnetStrength;
    [HideInInspector] public float MaxRadius;

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
        magnetSpawnPointTrans = GameObject.Find("BallSpawnPoint").GetComponent<Transform>();
    }

    private void MagnetAttach()
    {
        // Ensure this is called only when MaxPlaceableMagnets is not reached
        Vector3 magnetSpawnPointPos = magnetSpawnPointTrans.position;
        // To-doVector3 ballSpawnPointPos = ballSpawnPoint.GetComponent<Transform>().position;
        currentMagnet = bMagnetPooler.SpawnMagnetFromPool("Magnet", magnetSpawnPointPos, Quaternion.identity);
        currentMagnet.GetComponent<Transform>().SetParent(magnetSpawnPointTrans);
        currentMagnet.GetComponentInChildren<Canvas>().enabled = false;
        Debug.Log("Magnet Attached");
    }
}
