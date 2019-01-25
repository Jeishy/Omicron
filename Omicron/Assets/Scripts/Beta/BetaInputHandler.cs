using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaInputHandler : MonoBehaviour
{
    private BetaLevelManager betaManager;
    // Start is called before the first frame update
    private void Start()
    {
        betaManager = GetComponent<BetaLevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        Input();
    }

    private void Input()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            betaManager.MagnetPlace();
        }
    }
}
