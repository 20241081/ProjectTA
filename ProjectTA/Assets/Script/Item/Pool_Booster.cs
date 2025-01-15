using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Booster : MonoBehaviour
{
    public static Pool_Booster Instance;

    [SerializeField]
    private GameObject poolingObj;

    Queue<booster_Script> poolingObjectQueue = new Queue<booster_Script>();

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

    private booster_Script creatNewObj()
    {
        var newObj = Instantiate(poolingObj).GetComponent<booster_Script>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static booster_Script GetObject(int pos)
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            switch (pos)
            {
                case 0:
                    obj.transform.position = booster_Script.position01;
                    break;
                case 1:
                    obj.transform.position = booster_Script.position02;
                    break;
                case 2:
                    obj.transform.position = booster_Script.position03;
                    break;
            }
            return obj;
        }
        else
        {
            var newObj = Instance.creatNewObj();
            newObj.gameObject.SetActive(true);
            switch(pos)
            {
                case 0:
                    newObj.transform.position = booster_Script.position01;
                    break;
                case 1:
                    newObj.transform.position = booster_Script.position02;
                    break;
                case 2:
                    newObj.transform.position = booster_Script.position03;
                    break;
            }
            return newObj;
        }
    }

    public static void ReturnObject(booster_Script obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
