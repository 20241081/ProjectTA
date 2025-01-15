using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.PlayerSettings;

public class Pool_Item : MonoBehaviour
{
    [Header("코인 생성 쿨타임")]
    [SerializeField] private float coolTime_Coin;
    [Header("아이템 생성 쿨타임")]
    [SerializeField] private float coolTime_Item;

    private float deltaTime_coin;
    private float deltaTime_item;

    public Pool_Coin coinPool;
    public GameObject coinPtn01;
    public GameObject coinPtn02;
    public GameObject coinPtn03;
    public GameObject coinPtn04;
    void Start()
    {
        
    }

    void Update()
    {
        gen_Item();
        gen_coin();
    }

    private void gen_Item()
    {
        if (deltaTime_item >= coolTime_Item)
        {
            deltaTime_item = 0;
            //생성
            int item = Random.Range(0, 3);
            int pos = Random.Range(0, 3);
            switch (item)
            {
                case 0:
                    var Heal = Pool_Heal.GetObject(pos);
                    break;
                case 1:
                    var Boost = Pool_Booster.GetObject(pos);
                    break;
                case 2:
                    var Shield = Pool_Shield.GetObject(pos);
                    break;
            }    
        }
        else if (deltaTime_item < coolTime_Item)
        {
            deltaTime_item += Time.deltaTime;
        }
    }

    private void gen_coin()
    {
        if (deltaTime_coin > coolTime_Coin)
        {
            deltaTime_coin = 0;
            int pattern = Random.Range(0, 4);
            int pos = 0;
            switch (pattern)
            {
                case 0:
                    pos = Random.Range(0, 3);
                    GameObject ptn1 = coinPool.GetObject(coinPtn01);
                    switch (pos)
                    {
                        case 0:
                            ptn1.transform.position = new Vector3(-6, 4.5f, 20);
                            break;
                        case 1:
                            ptn1.transform.position = new Vector3(0, 4.5f, 20);
                            break;
                        case 2:
                            ptn1.transform.position = new Vector3(6, 4.5f, 20);
                            break;
                    }
                    break;
                case 1:
                    GameObject ptn2 = coinPool.GetObject(coinPtn02);
                    ptn2.transform.position = new Vector3(0, 0, 0);
                    break;
                case 2:
                    pos = Random.Range(0, 3);
                    GameObject ptn3 = coinPool.GetObject(coinPtn03);
                    switch (pos)
                    {
                        case 0:
                            ptn3.transform.position = new Vector3(-6, 4.5f, 20);
                            break;
                        case 1:
                            ptn3.transform.position = new Vector3(0, 4.5f, 20);
                            break;
                        case 2:
                            ptn3.transform.position = new Vector3(6, 4.5f, 20);
                            break;
                    }
                    break;
                case 3:
                    GameObject ptn4 = coinPool.GetObject(coinPtn04);
                    ptn4.transform.position = new Vector3(0, 0, 0);
                    break;
            }
        }
        else
        {
            deltaTime_coin += Time.deltaTime;
        }
    }
}
