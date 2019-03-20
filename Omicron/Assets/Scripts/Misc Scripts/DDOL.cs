using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script on DDOL GO
// All children GOs are not destroyed on load
public class DDOL : MonoBehaviour {

	private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
