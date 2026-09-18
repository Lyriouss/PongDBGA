using System.Collections.Generic;
using UnityEngine;

public class AudioObjectPooler : MonoBehaviour
{
    public static AudioObjectPooler instance;

    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private int initialPoolSize;

    public List<GameObject> pooledObjects;

    private void Awake()
    {
        //Creates a singleton instance of this class
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        //Makes a new instance of pool List
        pooledObjects = new List<GameObject>();

        //Instantiates a set number of inactive prefabs in pool on start
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    //Returns an inactive prefab to play an audio clip
    public GameObject GetPooledObject()
    {
        //Checks through all object in pool if there are any inactive objects to use
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        
        //If there are none then instantiates a new prefab and adds that to the pool
        GameObject obj = Instantiate(audioSourcePrefab, transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
