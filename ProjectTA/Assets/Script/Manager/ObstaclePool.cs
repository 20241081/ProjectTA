using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [System.Serializable]
    public class obstacles
    {
        public GameObject obstaclePrefab;
        public int size;
    }

    public List<obstacles> prefabs;

    private Dictionary<GameObject, Queue<GameObject>> pools;

    private void Start()
    {
        pools = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (var prefab in prefabs)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < prefab.size; i++)
            {
                GameObject obj = Instantiate(prefab.obstaclePrefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            pools.Add(prefab.obstaclePrefab, objectQueue);
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
        newObj.SetActive(true) ;
        return newObj;
    }

    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        pools[prefab].Enqueue(obj);
    }
}
