using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool_Heal : MonoBehaviour
{
    public static Pool_Heal Instance;

    [SerializeField]
    private GameObject poolingObj;

    Queue<Heal_Script> poolingObjectQueue = new Queue<Heal_Script>();

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Heal_Script CreateNewObject()
    {
        var newObj = Instantiate(poolingObj).GetComponent<Heal_Script>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Heal_Script GetObject(int pos)
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            switch (pos)
            {
                case 0:
                    obj.transform.position = Heal_Script.position01;
                    break;
                case 1:
                    obj.transform.position = Heal_Script.position02;
                    break;
                case 2:
                    obj.transform.position = Heal_Script.position03;
                    break;
            }
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            switch (pos)
            {
                case 0:
                    newObj.transform.position = Heal_Script.position01;
                    break;
                case 1:
                    newObj.transform.position = Heal_Script.position02;
                    break;
                case 2:
                    newObj.transform.position = Heal_Script.position03;
                    break;
            }
            return newObj;
        }
    }

    public static void ReturnObject(Heal_Script obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
