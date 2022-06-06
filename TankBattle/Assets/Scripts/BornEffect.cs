using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BornEffect : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemiesPrefabsList;
    public bool createrPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void BornTank()
    {
        if (createrPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int number = Random.Range(0, 2); //敌人数组的索引随机
            Instantiate(enemiesPrefabsList[number], transform.position, Quaternion.identity);
        }
    }
}