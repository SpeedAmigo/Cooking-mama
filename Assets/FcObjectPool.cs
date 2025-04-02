using System.Collections.Generic;
using UnityEngine;

public class FcObjectPool : MonoBehaviour
{
    public List<FlappyPool> pools;
    public List<GameObject> activeObjects = new();
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (FlappyPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.tag = pool.tag;
                obj.GetComponent<FlappyCakeBackgroundController>().scrollSpeed = pool.scrollSpeed;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist");
            return null;
        }

        if (poolDictionary[tag].Count > 0)
        {
            GameObject obj = poolDictionary[tag].Dequeue();
            obj.SetActive(true);
            activeObjects.Add(obj);
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool is to small");
            return null;
        }
    }

    public void ReturnObject(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist");
            return;
        }
        
        activeObjects.Remove(obj);
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}

[System.Serializable]
public class FlappyPool
{
    public string tag;
    public GameObject prefab;
    [Range(0f,1f)] public float scrollSpeed;
    public int poolSize;
}
