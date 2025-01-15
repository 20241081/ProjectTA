using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Pool_Coin : MonoBehaviour
{
    [System.Serializable]
    public class coinPattern
    {
        public GameObject prefab;
        public int size;
    }

    public List<coinPattern> patterns;

    private Dictionary<GameObject, Queue<GameObject>> pools;

    private void Start()
    {
        pools = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (var coinPattern in patterns)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < coinPattern.size; i++)
            {
                GameObject obj = Instantiate(coinPattern.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            pools.Add(coinPattern.prefab, objectQueue);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (pools.ContainsKey(prefab) && pools[prefab].Count > 0)
        {
            GameObject obj = pools[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        pools[prefab].Enqueue(obj);
    }
}
