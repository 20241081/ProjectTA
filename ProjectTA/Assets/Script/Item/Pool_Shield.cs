using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Shield : MonoBehaviour
{
    public static Pool_Shield Instance;

    [SerializeField]
    private GameObject poolingObj;

    Queue<Shield_Script> poolingObjectQueue = new Queue<Shield_Script>();

    private void Awake()
    {
        Instance = this;

        init(10);
    }

    private void init(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(creatNewObj());
        }
    }

    private Shield_Script creatNewObj()
    {
        var newObj = Instantiate(poolingObj).GetComponent<Shield_Script>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Shield_Script GetObject(int pos)
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            switch (pos)
            {
                case 0:
                    obj.transform.position = Shield_Script.position01;
                    break;
                case 1:
                    obj.transform.position = Shield_Script.position02;
                    break;
                case 2:
                    obj.transform.position = Shield_Script.position03;
                    break;
            }
            return obj;
        }
        else
        {
            var newObj = Instance.creatNewObj();
            newObj.gameObject.SetActive(true);
            switch (pos)
            {
                case 0:
                    newObj.transform.position = Shield_Script.position01;
                    break;
                case 1:
                    newObj.transform.position = Shield_Script.position02;
                    break;
                case 2:
                    newObj.transform.position = Shield_Script.position03;
                    break;
            }
            return newObj;
        }
    }

    public static void ReturnObject(Shield_Script obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
