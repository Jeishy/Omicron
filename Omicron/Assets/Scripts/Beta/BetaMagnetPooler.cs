using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnetPooler : MonoBehaviour
{
    // Class for pooler, with a tag, specific prefab to be in pool
    // and size of the pool
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region MagnetPooler Singleton
    public static BetaMagnetPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;                                        // List of the pools
    public Dictionary<string, Queue<GameObject>> poolDictionary;    // Dictionary of pools that pools are stored

	// Use this for initialization
	private void Start () {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Store each pool in pools list in poolsDictionary dictionary
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                // Instantiate number of specified prefabs from pool
                GameObject obj = Instantiate(pool.prefab);
                // Set them all to false
                obj.SetActive(false);
                // Store object in objectPool queue
                objectPool.Enqueue(obj);
            }
        
            // Adds pool to dicionary
            poolDictionary.Add(pool.tag, objectPool);
            // Do null check in case poolDictionary is empty
            if (poolDictionary == null)
                Debug.Log("poolDictionary is null!");
        }
	}


    public GameObject SpawnMagnetFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        // Null check for if the parsed in tag isn't in the dictionary
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject magnetToSpawn = poolDictionary[tag].Dequeue();
        magnetToSpawn.SetActive(true);                              // Set active to true
        magnetToSpawn.transform.position = position;                // Sets position to parsed in position
        magnetToSpawn.transform.rotation = rotation;                // Sets rotation to parsed in rotation
        poolDictionary[tag].Enqueue(magnetToSpawn);

        return magnetToSpawn;
    }
}
