using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaMagnetPooler : MonoBehaviour
{
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

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

	// Use this for initialization
	private void Start () {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
            if (poolDictionary == null)
                Debug.Log("poolDictionary is null!");
        }
	}


    public GameObject SpawnMagnetFromPool (string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject magnetToSpawn = poolDictionary[tag].Dequeue();
        magnetToSpawn.SetActive(true);
        magnetToSpawn.transform.position = position;
        magnetToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(magnetToSpawn);

        return magnetToSpawn;
    }
}
