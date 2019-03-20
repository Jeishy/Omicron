using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaInputHandler : MonoBehaviour
{

    private DeltaLevelManager _deltaManager;
    // Start is called before the first frame update
    private void Start()
    {
        _deltaManager = GetComponent<DeltaLevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void Input()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTrackedRemote))
        {
            _deltaManager.PhotonShoot();
        }
    }
}
